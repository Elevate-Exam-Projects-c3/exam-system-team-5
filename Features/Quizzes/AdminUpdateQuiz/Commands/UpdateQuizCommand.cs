using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Quizzes.AdminUpdateQuiz.Commands
{
    public record UpdateQuizCommand(
        Guid QuizId,
        string Title,
        string? Instructions,
        int DurationMinutes,
        DateTime StartDate,
        DateTime EndDate,
        int? MaxAttempts = null,
        int PassScore = 60
        ) : IRequest<RequestResponse<bool>>;

}
