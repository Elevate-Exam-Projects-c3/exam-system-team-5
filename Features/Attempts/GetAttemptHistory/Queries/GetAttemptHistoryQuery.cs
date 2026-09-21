using exam_system.Features.Attempts.GetAttemptHistory.Models;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Attempts.GetAttemptHistory.Queries
{
    public record GetAttemptHistoryQuery(
    Guid StudentId,
    int PageIndex = 1,
    int PageSize = 10
) : IRequest<RequestResponse<PaginatedResult<AttemptHistoryResponse>>>;
}
