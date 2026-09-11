using exam_system.Features.Quizzes.AdminCreateQuiz.DTOS;
using exam_system.Features.Quizzes.AdminUpdateQuiz.DTOs;
using exam_system.Features.Shared;
using exam_system.Features.Shared.DTOs;
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
