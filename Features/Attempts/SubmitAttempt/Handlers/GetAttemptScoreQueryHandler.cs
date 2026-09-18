using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.SubmitAttempt.Queries;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Attempts.SubmitAttempt.Handlers
{
    public class GetAttemptScoreQueryHandler(IGenericRepository<StudentQuestionAnswer> repository) : IRequestHandler<GetAttemptScoreQuery, RequestResponse<double>>
    {
        public async Task<RequestResponse<double>> Handle(GetAttemptScoreQuery request, CancellationToken cancellationToken)
        {
            var count = await repository.CountAsync(x => x.AttemptId == request.attemptId && x.IsCorrect == true);
            return RequestResponse<double>.Ok(count);
        }
    }
}
