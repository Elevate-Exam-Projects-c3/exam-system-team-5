using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Diplomas.GetStudentDashboard.Dtos;
using exam_system.Features.Diplomas.GetStudentDashboard.Queries;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Diplomas.GetStudentDashboard.Handlers
{
    public class GetStudentAttemptsQueryHandler : IRequestHandler<GetStudentAttemptsQuery, RequestResponse<List<StudentAttemptDto>>>
    {
        private readonly IGenericRepository<QuizAttempt> _attemptRepository;
        public GetStudentAttemptsQueryHandler(IGenericRepository<QuizAttempt> attemptRepository)
            => _attemptRepository = attemptRepository;
        public async Task<RequestResponse<List<StudentAttemptDto>>> Handle(GetStudentAttemptsQuery request, CancellationToken cancellationToken)
        {
            var attempts = await _attemptRepository
                .Get(a => a.StudentId == request.StudentId)
                .ProjectToType<StudentAttemptDto>()
                .ToListAsync(cancellationToken);

            return RequestResponse<List<StudentAttemptDto>>.Ok(attempts);
        }
    }
}
