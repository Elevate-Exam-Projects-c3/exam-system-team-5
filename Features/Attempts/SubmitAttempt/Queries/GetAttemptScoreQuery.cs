using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Attempts.SubmitAttempt.Queries
{
    public record GetAttemptScoreQuery(Guid attemptId) : IRequest<RequestResponse<double>>;
  
}
