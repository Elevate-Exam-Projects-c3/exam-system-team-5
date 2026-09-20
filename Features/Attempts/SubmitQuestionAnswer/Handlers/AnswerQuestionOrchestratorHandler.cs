using exam_system.Features.Attempts.SubmitQuestionAnswer.Commands;
using exam_system.Features.Attempts.SubmitQuestionAnswer.Orchestrators;
using exam_system.Features.Attempts.SubmitQuestionAnswer.Queries;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Handlers
{
    public class AnswerQuestionOrchestratorHandler : IRequestHandler<AnswerQuestionOrchestrator, RequestResponse<uint>>
    {
        private IMediator _mediator;

        public AnswerQuestionOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<RequestResponse<uint>> Handle(AnswerQuestionOrchestrator request, CancellationToken cancellationToken)
        {
          var attempt = await _mediator.Send(new GetAttemptByIdQuery(request.AttemptId), cancellationToken);
            var option = await _mediator.Send(new GetOptionByIdQuery(request.SelectedOptionId), cancellationToken);
            var question = await _mediator.Send(new GetQuestionByIdQuery(request.QuestionId), cancellationToken);
             await _mediator.Send(new CheckDeadlineQuery(attempt.Deadline), cancellationToken);
            
            await _mediator.Send(new AddQuestionAnswerCommand(request.QuestionId, option.Data, request.AttemptId), cancellationToken);


            return RequestResponse<uint>.Ok(1, "Answer submitted successfully");
        }
    }
} 
