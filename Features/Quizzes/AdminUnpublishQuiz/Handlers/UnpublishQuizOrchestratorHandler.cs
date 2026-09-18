using exam_system.Features.Quizzes.AdminUnpublishQuiz.Commands;
using exam_system.Features.Quizzes.AdminUnpublishQuiz.Orchestrators;
using exam_system.Features.Quizzes.AdminUnpublishQuiz.Queries;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Quizzes.AdminUnpublishQuiz.Handlers
{
    public class UnpublishQuizOrchestratorHandler
        : IRequestHandler<UnpublishQuizOrchestrator,RequestResponse<Unit>>
    {
        private readonly IMediator _mediator;

        public UnpublishQuizOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<RequestResponse<Unit>> Handle(
            UnpublishQuizOrchestrator request,
            CancellationToken cancellationToken)
        {
            // Check if there are any in-progress attempts
            await _mediator.Send(
                new CheckInProgressAttemptsQuery(request.QuizId),
                cancellationToken);

            // If no in-progress attempts, unpublish the quiz
            await _mediator.Send(
                new UnpublishQuizCommand(request.QuizId),
                cancellationToken);

            return RequestResponse<Unit>.Ok(Unit.Value, "Quiz successfully unpublished.");
        }
    }
}
