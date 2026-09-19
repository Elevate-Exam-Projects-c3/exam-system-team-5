using exam_system.Common.Enums;
using exam_system.Domain.Entities.Attempts;
using exam_system.Domain.Entities.Identity;
using exam_system.Domain.Entities.Quizzes;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Dtos
{
    public class QuizAttemptDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid QuizId { get; set; }
        public DateTime Deadline { get; set; }
        public AttemptStatus Status { get; set; } = AttemptStatus.InProgress;
    }
}
