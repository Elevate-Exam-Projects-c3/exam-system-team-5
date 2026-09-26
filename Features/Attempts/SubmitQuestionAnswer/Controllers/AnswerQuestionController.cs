using exam_system.Features.Attempts.SubmitQuestionAnswer.Orchestrators;
using exam_system.Features.Attempts.SubmitQuestionAnswer.ViewModels;
using exam_system.Features.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class AnswerQuestionController : ControllerBase
    {

        private readonly IMediator _mediator;
        public AnswerQuestionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{attemptId}/answers")]
        public async Task<IActionResult> AnswerQuestion(
            Guid attemptId,
            [FromBody] AnswerQuestionViewModel model,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new AnswerQuestionOrchestrator(
                    attemptId,
                    model.QuestionId,
                    model.SelectedOptionId),
                cancellationToken);

            var response = EndpointResponse<uint>.FromResult(result);

            return StatusCode(response.StatusCode, response);
        }
    }
}
