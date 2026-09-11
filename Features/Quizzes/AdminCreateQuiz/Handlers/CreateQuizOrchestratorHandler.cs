using exam_system.Features.Quizzes.AdminCreateQuiz.Commands;
using exam_system.Features.Quizzes.AdminCreateQuiz.Orchestrators;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Handlers
{
    public class CreateQuizOrchestratorHandler(IMediator mediator) : IRequestHandler<CreateQuizOrchestrator , RequestResponse<bool>>
    {
        public async Task<RequestResponse<bool>> Handle(CreateQuizOrchestrator request, CancellationToken cancellationToken=default)
        {
            //chick if this diploma exist or not

            // then call the handler to create the quiz
            var result = await mediator.Send(new CreateQuizCommand(
                request.DiplomaID,
                request.Instructions,
                request.Title,
                request.DurationMinutes,
                request.StartDate,
                request.EndDate,
                request.PassScore,
                request.MaxAttempts), cancellationToken);
            return result;
        }
    }
}
