using exam_system.Features.Quizzes.AdminCreateQuiz.Commands;
using exam_system.Features.Quizzes.AdminCreateQuiz.DTOS;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Orchestrators
{
    //public interface CreateQuizOrchestrator : IRequest<RequestResponse<bool>>
    //{
    //    public CreateQuizCommand CreateQuizCommand { get; set; }
    //}
    public record CreateQuizOrchestrator(CreateQuizCommand CreateQuizCommand) : IRequest<RequestResponse<bool>>;

}
