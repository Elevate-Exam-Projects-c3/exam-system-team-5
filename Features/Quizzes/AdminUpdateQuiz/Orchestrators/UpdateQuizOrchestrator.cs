using exam_system.Features.Quizzes.AdminUpdateQuiz.Commands;
using exam_system.Features.Shared;
using exam_system.Features.Shared.DTOs;
using MediatR;

namespace exam_system.Features.Quizzes.AdminUpdateQuiz.Orchestrators
{
    public record UpdateQuizOrchestrator (UpdateQuizCommand UpdateQuizCommand) : IRequest<RequestResponse<QuizResponseDto>>;
    
}
