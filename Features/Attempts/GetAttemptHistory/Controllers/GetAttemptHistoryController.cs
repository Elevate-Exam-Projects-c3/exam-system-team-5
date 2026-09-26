using exam_system.Common.Constants;
using exam_system.Features.Attempts.GetAttemptHistory.Models;
using exam_system.Features.Attempts.GetAttemptHistory.Queries;
using exam_system.Features.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace exam_system.Features.Attempts.GetAttemptHistory.Controllers
{
    [ApiController]
    [Route("api/students/me/attempts")]
    [Authorize(Policy = AppPolicies.RequireStudent)]
    public class GetAttemptHistoryController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAttemptHistory([FromQuery] int pageIndex = 1,[FromQuery] int pageSize = 10,CancellationToken cancellationToken = default)
        {
            var studentIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(studentIdString))
                return Unauthorized();

            if (!Guid.TryParse(studentIdString, out var studentId))
            {
                return BadRequest(new
                {
                    Message = "Invalid user identifier"
                });
            }

            var result = await mediator.Send(
                new GetAttemptHistoryQuery(studentId,pageIndex,pageSize),cancellationToken);

            var response =
                EndpointResponse<PaginatedResult<AttemptHistoryResponse>>.FromResult(result);

            return StatusCode(response.StatusCode,response);
        }
    }
}
