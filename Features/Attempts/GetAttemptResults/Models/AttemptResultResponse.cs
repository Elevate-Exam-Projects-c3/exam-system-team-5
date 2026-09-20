using exam_system.Common.Enums;

namespace exam_system.Features.Attempts.GetAttemptResults.Models
{
    public class AttemptResultResponse
    {
        public Guid AttemptId { get; set; }

        public AttemptStatus Status { get; set; }

        public double? Score { get; set; }

        public bool? Passed { get; set; }

        public List<QuestionResultResponse> Questions { get; set; } = new();
    }
}
