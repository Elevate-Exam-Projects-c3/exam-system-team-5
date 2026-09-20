using exam_system.Features.Quizzes.AdminManageQuestions.Commands;
using exam_system.Features.Quizzes.AdminManageQuestions.Orchestrators;
using exam_system.Features.Quizzes.AdminManageQuestions.Queries;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Quizzes.AdminManageQuestions.Handlers
{
    public class AddQuestionOrchestratorHandler :
        IRequestHandler<AddQuestionOrchestrator, RequestResponse<Unit>>
    {
        private readonly IMediator _mediator;

        public AddQuestionOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<RequestResponse<Unit>> Handle(
            AddQuestionOrchestrator request,
            CancellationToken cancellationToken)
        {
          
            await _mediator.Send(
                new GetQuizByIdQuery(request.QuizId),
                cancellationToken);

        
            var questionId = Guid.NewGuid();

           
            await _mediator.Send(
                new AddQuestionCommand(
                    questionId,
                    request.QuizId,
                    request.Text,
                    request.Explanation,
                    request.OrderIndex),
                cancellationToken);

            // 4. Add Options
            var options = request.Options
                .Select(option => new AddOptionsCommand.AddOption(
                    option.OptionText,
                    option.IsCorrect))
                .ToList();

            await _mediator.Send(
                new AddOptionsCommand(
                    questionId,
                    options),
                cancellationToken);

            return RequestResponse<Unit>.Ok(
                Unit.Value,
                "Question added successfully.");
        }
    }
}
