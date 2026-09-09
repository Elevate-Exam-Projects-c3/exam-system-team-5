using exam_system.Features.Quizzes.AdminCreateQuiz.DTOS;
using exam_system.Features.Shared;
using exam_system.Features.Shared.DTOs;
using MediatR;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Commands
{
    //public class CreateQuizCommand : IRequest<RequestResponse<bool>>
    //{
    //    public Guid DiplomaID { get; set; }
    //    public CreateQuizRequestDto Dto { get; set; }

    //}
    public record CreateQuizCommand(Guid DiplomaID, CreateQuizRequestDto Dto) : IRequest<RequestResponse<bool>>;
}
