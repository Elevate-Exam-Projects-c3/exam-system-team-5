using exam_system.Common.Enums;
using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.BrowseDiplomas.Dtos;
using exam_system.Features.Diplomas.BrowseDiplomas.Queries;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Diplomas.BrowseDiplomas.Handlers
{

    public class GetDiplomasQueryHandler : IRequestHandler<GetDiplomasQuery, RequestResponse<PaginatedResult<DiplomaItemsResponse>>>
    {
        private readonly IGenericRepository<Diploma> _diplomaRepository;

        public GetDiplomasQueryHandler(IGenericRepository<Diploma> diplomaRepository)
        {
            _diplomaRepository = diplomaRepository;
        }

        public async Task<RequestResponse<PaginatedResult<DiplomaItemsResponse>>> Handle(GetDiplomasQuery request, CancellationToken cancellationToken)
        {
            // count of published diplomas
            var diplomaCount = await _diplomaRepository.CountAsync(a => a.Quizzes.Any(q => q.Status == QuizStatus.Published));


            // return studentprogress number of enterd quizes inthis diploma that enrolled on it
            // calculate the quiz status with submitte and timeout based on userstory EXAM-21
            var diplomasResponseItems = await _diplomaRepository.Get(a => a.Quizzes.Any(q => q.Status == QuizStatus.Published))
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .OrderBy(d => d.Title)
                .Select(d => new DiplomaItemsResponse(
                d.Id,
                d.Title,
                d.Description,
                d.Quizzes.Count(q => q.Status == QuizStatus.Published && q.Attempts.Any(a => a.StudentId == request.StudentId &&
                        (a.Status == AttemptStatus.Submitted || a.Status == AttemptStatus.TimedOut))),
                d.Quizzes.Count(q => q.Status == QuizStatus.Published)))
                .ToListAsync(cancellationToken);

            var diplomaList = new PaginatedResult<DiplomaItemsResponse>(diplomasResponseItems, diplomaCount, request.PageIndex, request.PageSize);

            return RequestResponse<PaginatedResult<DiplomaItemsResponse>>.Ok(diplomaList);


        }
    }
}
