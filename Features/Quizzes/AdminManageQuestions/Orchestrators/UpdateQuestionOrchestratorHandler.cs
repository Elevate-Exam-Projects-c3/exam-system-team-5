using exam_system.Features.Quizzes.AdminManageQuestions.Commands;
using MediatR;

namespace exam_system.Features.Quizzes.AdminManageQuestions.Orchestrators
{
    public class UpdateQuestionOrchestratorHandler
        : IRequestHandler<UpdateQuestionOrchestrator, Unit>
    {
        private readonly IMediator _mediator;

        public UpdateQuestionOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Unit> Handle(
            UpdateQuestionOrchestrator request,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new UpdateQuestionCommand(
                    request.QuestionId,
                    request.Text,
                    request.Explanation,
                    request.OrderIndex),
                cancellationToken);

            var options = request.Options
                .Select(option =>
                    new UpdateOptionsCommand.UpdateOption(
                        option.OptionText,
                        option.IsCorrect))
                .ToList();

            await _mediator.Send(
                new UpdateOptionsCommand(
                    request.QuestionId,
                    options),
                cancellationToken);

            return Unit.Value;
        }
    }
}
