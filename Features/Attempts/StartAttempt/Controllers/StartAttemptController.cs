using exam_system.Features.Attempts.StartAttempt.Orchestrators;
using exam_system.Features.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace exam_system.Features.Attempts.StartAttempt.Controllers
{
    [ApiController]
    [Route("api/quizzes/{QuizId}/attempts")]
    public class StartAttemptController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> StartAttempt([FromRoute] Guid QuizId, CancellationToken cancellationToken = default)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized();

            if (!Guid.TryParse(userIdString, out var userGuidId))
                return BadRequest(new { Message = "Invalid user identifier" });

            var result = await mediator.Send(new StartAttemptOrchestrator(QuizId, userGuidId), cancellationToken);

            var response = EndpointResponse<bool>.FromResult(result);
            return StatusCode(response.StatusCode, response);

        }
    }    
}
