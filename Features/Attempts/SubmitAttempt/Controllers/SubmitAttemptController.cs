using exam_system.Features.Attempts.SubmitAttempt.Orchestrators;
using exam_system.Features.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Attempts.SubmitAttempt.Controllers
{
    [ApiController]
    [Route("api/attempts/{id}/submit")]
    public class SubmitAttemptController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SubmitAttempt([FromRoute] Guid id,CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new SubmitAttemptOrchestrator(id), cancellationToken);
            var response=EndpointResponse<bool>.FromResult(result);
            return StatusCode(response.StatusCode, response);
        }
    }
}
