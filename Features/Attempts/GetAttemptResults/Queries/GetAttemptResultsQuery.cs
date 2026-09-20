using exam_system.Features.Attempts.GetAttemptResults.Models;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Attempts.GetAttemptResults.Queries
{
    public record GetAttemptResultsQuery(
       Guid AttemptId,
       Guid StudentId
   ) : IRequest<RequestResponse<AttemptResultResponse>>;
}
