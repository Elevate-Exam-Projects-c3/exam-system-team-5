using exam_system.Common.Enums;
using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Diplomas.GetDiplomaDetail.Dtos;
using exam_system.Features.Diplomas.GetDiplomaDetail.Queries;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Diplomas.GetDiplomaDetail.Handlers
{
    public class GetPublishedQuizzesByDiplomaQueryHandler : IRequestHandler<GetPublishedQuizzesByDiplomaQuery, RequestResponse<List<PublishedQuizDto>>>
    {
        private readonly IGenericRepository<Quiz> _quizRepository;
        public GetPublishedQuizzesByDiplomaQueryHandler(IGenericRepository<Quiz> quizRepository)
            => _quizRepository = quizRepository;

        public async Task<RequestResponse<List<PublishedQuizDto>>> Handle(GetPublishedQuizzesByDiplomaQuery request, CancellationToken cancellationToken)
        {
            var quizzes = await _quizRepository
                .Get(q => q.DiplomaId == request.DiplomaId && q.Status == QuizStatus.Published)
                .ProjectToType<PublishedQuizDto>()
                .ToListAsync(cancellationToken);

            return RequestResponse<List<PublishedQuizDto>>.Ok(quizzes);
        }
    }
}
