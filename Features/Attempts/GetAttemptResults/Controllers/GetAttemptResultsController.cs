using exam_system.Common.Constants;
using exam_system.Features.Attempts.GetAttemptResults.Models;
using exam_system.Features.Attempts.GetAttemptResults.Queries;
using exam_system.Features.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace exam_system.Features.Attempts.GetAttemptResults.Controllers
{
    [ApiController]
    [Route("api/attempts/{id}/results")]
    [Authorize(Policy = AppPolicies.RequireStudent)]

    public class GetAttemptResultsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAttemptResults([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var StudentIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(StudentIdString))
                return Unauthorized();

            if (!Guid.TryParse(StudentIdString, out var studentId))
                return BadRequest(new
                {
                    Message = "Invalid user identifier"
                });
            var result = await mediator.Send(new GetAttemptResultsQuery(id, studentId), cancellationToken);
            var response = EndpointResponse<AttemptResultResponse>.FromResult(result);
            return StatusCode(response.StatusCode, response);

        }


    }
}
