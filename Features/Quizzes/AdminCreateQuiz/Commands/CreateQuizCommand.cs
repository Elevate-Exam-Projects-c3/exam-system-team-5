using exam_system.Features.Quizzes.AdminCreateQuiz.DTOS;
using exam_system.Features.Shared;
using exam_system.Features.Shared.DTOs;
using MediatR;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Commands
{
    public record CreateQuizCommand(
        Guid DiplomaID,
        string Title,
        string? Instructions,
        int DurationMinutes,
        DateTime StartDate,
        DateTime EndDate,
        int PassScore,
        int? MaxAttempts
        ) : IRequest<RequestResponse<bool>>;
}
