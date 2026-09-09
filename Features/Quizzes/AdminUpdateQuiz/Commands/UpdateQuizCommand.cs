using exam_system.Features.Quizzes.AdminCreateQuiz.DTOS;
using exam_system.Features.Quizzes.AdminUpdateQuiz.DTOs;
using exam_system.Features.Shared;
using exam_system.Features.Shared.DTOs;
using MediatR;

namespace exam_system.Features.Quizzes.AdminUpdateQuiz.Commands
{
    public record UpdateQuizCommand(Guid QuizId, UpdateQuizRequestDto Dto) : IRequest<RequestResponse<QuizResponseDto>>;

}
