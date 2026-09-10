using exam_system.Common.Enums;
using exam_system.Domain.Entities.Diplomas;
using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Diplomas.BrowseDiplomas.Dtos;
using exam_system.Features.Diplomas.BrowseDiplomas.Queries;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Diplomas.BrowseDiplomas.Handlers
{

    public class GetDiplomasQueryHandler : IRequestHandler<GetDiplomasQuery, RequestResponse<PaginatedResult<DiplomaItemsResponseDto>>>
    {
        private readonly IGenericRepository<Diploma> _diplomaRepository;
        private readonly IGenericRepository<Quiz> _quizRepository;

        public GetDiplomasQueryHandler(IGenericRepository<Diploma> diplomaRepository , IGenericRepository<Quiz> quizRepository)
        {
            _diplomaRepository = diplomaRepository;
            _quizRepository = quizRepository;
        }

        // Perf fix: replaced correlated subqueries (Count() per row in projection) with 
        // separate GroupBy query on Quizzes, scoped to the current page's DiplomaIds.
        // This avoids re-scanning Quizzes per diploma row and computing TotalQuizzes twice.

        public async Task<RequestResponse<PaginatedResult<DiplomaItemsResponseDto>>> Handle(GetDiplomasQuery request, CancellationToken cancellationToken)
        {
            var query = _diplomaRepository.Get(d => d.Quizzes.Any(q => q.Status == QuizStatus.Published));

            // count of published diplomas
            var diplomaCount = await query.CountAsync(cancellationToken);

            // return studentprogress number of enterd quizes inthis diploma that enrolled on it
            // calculate the quiz status with submitte and timeout based on userstory EXAM-21

            var pagedDiplomas = await query
                .OrderBy(d => d.Title)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(d => new { d.Id, d.Title, d.Description })
                .ToListAsync(cancellationToken);

            var diplomaIds = pagedDiplomas.Select(d => d.Id).ToList();

            var quizStats = await _quizRepository.Get(q => diplomaIds.Contains(q.DiplomaId) && q.Status == QuizStatus.Published)
                .GroupBy(q => q.DiplomaId)
                .Select(g => new
                {
                    DiplomaId = g.Key,
                    TotalQuizzes = g.Count(),
                    CompletedQuizzesCount = g.Count(q => q.Attempts.Any(a => a.StudentId == request.StudentId
                        && (a.Status == AttemptStatus.Submitted || a.Status == AttemptStatus.TimedOut)))
                }).ToListAsync(cancellationToken);

            var diplomasResponseItems = pagedDiplomas
                .Select(d =>
                {
                    var stats = quizStats.FirstOrDefault(s => s.DiplomaId == d.Id);
                    return new DiplomaItemsResponseDto(
                        d.Id, 
                        d.Title, 
                        d.Description,
                        stats.CompletedQuizzesCount,
                        stats.TotalQuizzes);
                }).ToList();

            var diplomaList = new PaginatedResult<DiplomaItemsResponseDto>(diplomasResponseItems, diplomaCount, request.PageIndex, request.PageSize);

            return RequestResponse<PaginatedResult<DiplomaItemsResponseDto>>.Ok(diplomaList);


        }
    }
}
