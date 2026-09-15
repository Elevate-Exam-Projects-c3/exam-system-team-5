using exam_system.Common.Enums;
using exam_system.Common.Middleware;
using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminPublishQuiz.Commands;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.Orchestrators;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.Queries;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Quizzes.AdminPublishQuiz.Handlers
{
    public class PublishQuizOrchestratorHandler
        : IRequestHandler<PublishQuizOrchestrator, Unit>
    {
        private readonly IMediator _mediator;

        public PublishQuizOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Unit> Handle(
            PublishQuizOrchestrator request,
            CancellationToken cancellationToken)
        {
            var readiness = await _mediator.Send(
                new QuizReadinessOrchestrator(request.QuizId),
                cancellationToken);

            if (!readiness.IsReady)
            {
                var errors = string.Join(
                    " ",
                    readiness.Checks
                        .Where(check => !check.Passed)
                        .Select(check => check.Message));

                throw new ConflictException(
                    $"Quiz cannot be published. {errors}");
            }

            await _mediator.Send(
                new PublishQuizCommand(request.QuizId),
                cancellationToken);

            return Unit.Value;
        }
    }
}
