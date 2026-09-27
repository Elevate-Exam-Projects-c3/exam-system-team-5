using exam_system.Features.Attempts.CheckRemainingTime.Dtos;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Attempts.CheckRemainingTime.Orchestrators
{
    public record CheckRemainingTimeOrchestrator(Guid AttemptId, Guid StudentId): IRequest<RequestResponse<TimeRemainingResponseDto>>;
}
