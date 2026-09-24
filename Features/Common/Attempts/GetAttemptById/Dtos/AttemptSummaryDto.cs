using exam_system.Common.Enums;

namespace exam_system.Features.Common.Attempts.GetAttemptById.Dtos
{
    public record AttemptSummaryDto(Guid Id, Guid StudentId, AttemptStatus Status, DateTime Deadline);

}
