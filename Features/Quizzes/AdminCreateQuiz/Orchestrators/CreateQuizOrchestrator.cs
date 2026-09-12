using exam_system.Features.Quizzes.AdminCreateQuiz.Commands;
using exam_system.Features.Quizzes.AdminCreateQuiz.DTOS;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Orchestrators
{
    
    public record CreateQuizOrchestrator(
        Guid DiplomaID,
        string Title,
        string? Instructions,
        int DurationMinutes,
        DateTime StartDate,
        DateTime EndDate,
        int PassScore,
        int? MaxAttempts) : IRequest<RequestResponse<bool>>;

}
