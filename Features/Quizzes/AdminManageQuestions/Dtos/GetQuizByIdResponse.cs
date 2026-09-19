using exam_system.Common.Enums;

namespace exam_system.Features.Quizzes.AdminManageQuestions.Dtos
{
    public class GetQuizByIdResponse
    {
        public Guid QuizId { get; set; }
        public QuizStatus Status { get; set; }
    }
}
