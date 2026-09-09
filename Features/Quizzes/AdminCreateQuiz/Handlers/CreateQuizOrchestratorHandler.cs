using exam_system.Features.Quizzes.AdminCreateQuiz.Commands;
using exam_system.Features.Quizzes.AdminCreateQuiz.Orchestrators;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Handlers
{
    public class CreateQuizOrchestratorHandler(IMediator mediator) : IRequestHandler<CreateQuizCommand, RequestResponse<bool>>
    {
        public async Task<RequestResponse<bool>> Handle(CreateQuizCommand request, CancellationToken cancellationToken=default)
        {

            // then call the handler to create the quiz
            var result = await mediator.Send(request, cancellationToken);
            return result;
        }
    }
}
