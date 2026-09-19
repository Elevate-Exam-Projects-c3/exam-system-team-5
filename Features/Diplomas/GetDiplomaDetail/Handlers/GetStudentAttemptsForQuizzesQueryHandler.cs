using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Diplomas.GetDiplomaDetail.Dtos;
using exam_system.Features.Diplomas.GetDiplomaDetail.Queries;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Diplomas.GetDiplomaDetail.Handlers
{
    public class GetStudentAttemptsForQuizzesQueryHandler : IRequestHandler<GetStudentAttemptsForQuizzesQuery, RequestResponse<List<StudentQuizAttemptDto>>>
    {
        private readonly IGenericRepository<QuizAttempt> _attemptRepository;
        public GetStudentAttemptsForQuizzesQueryHandler(IGenericRepository<QuizAttempt> attemptRepository)
            => _attemptRepository = attemptRepository;

        public async Task<RequestResponse<List<StudentQuizAttemptDto>>> Handle(GetStudentAttemptsForQuizzesQuery request, CancellationToken cancellationToken)
        {
            var attempts = await _attemptRepository
                .Get(a => a.StudentId == request.StudentId && request.QuizIds.Contains(a.QuizId))
                .ProjectToType<StudentQuizAttemptDto>()
                .ToListAsync(cancellationToken);

            return RequestResponse<List<StudentQuizAttemptDto>>.Ok(attempts);
        }
    }
}
