using exam_system.Features.Quizzes.AdminUpdateQuiz.Commands;
using exam_system.Features.Shared;
using exam_system.Features.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;

namespace exam_system.Features.Quizzes.AdminUpdateQuiz.Handlers
{
    public class UpdateQuizOrchestratorHandler(IMediator mediator) : IRequestHandler<UpdateQuizCommand, RequestResponse<QuizResponseDto>>
    {
        public async Task<RequestResponse<QuizResponseDto>> Handle(UpdateQuizCommand request, CancellationToken cancellationToken=default)
        {
            // here check if the diploma exists or not to create a quiz for this diploma

            // then call the handler to Update the quiz
            var result = await mediator.Send(request, cancellationToken);
            return result;
        }
    }
}
