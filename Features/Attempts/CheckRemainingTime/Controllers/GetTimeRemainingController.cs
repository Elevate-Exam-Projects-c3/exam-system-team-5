using exam_system.Common.CurrentUser;
using exam_system.Features.Attempts.CheckRemainingTime.Controllers.ViewModels;
using exam_system.Features.Attempts.CheckRemainingTime.Orchestrators;
using exam_system.Features.Shared;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Attempts.CheckRemainingTime.Controllers
{
    [ApiController]
    [Route("api/attempts")]
    //[Authorize(Roles = "Student")]
    public class GetTimeRemainingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUser _currentUser;

        public GetTimeRemainingController(IMediator mediator, ICurrentUser currentUser)
        {
            _mediator = mediator;
            _currentUser = currentUser;
        }


        [HttpGet("{attemptId:guid}/time-remaining")]
        public async Task<IActionResult> GetTimeRemaining(Guid attemptId, CancellationToken cancellationToken)
        {
            var remainingTime = await _mediator.Send(
                new CheckRemainingTimeOrchestrator(attemptId, _currentUser.UserId), cancellationToken);

            var remainingTimeResult = remainingTime.Data.Adapt<TimeRemainingResponseViewModel>();

            var response = EndpointResponse<TimeRemainingResponseViewModel>.FromResult(RequestResponse<TimeRemainingResponseViewModel>
                .Ok(remainingTimeResult));

            return StatusCode(response.StatusCode, response);
        }
    }
}
