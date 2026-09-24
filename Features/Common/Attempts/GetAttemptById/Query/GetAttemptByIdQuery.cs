using exam_system.Features.Common.Attempts.GetAttemptById.Dtos;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Common.Attempts.GetAttemptById.Query
{
    public record GetAttemptByIdQuery(Guid AttemptId) : IRequest<RequestResponse<AttemptSummaryDto>>;
}
