using exam_system.Features.Analytics.SearchAttempts.Queries;
using exam_system.Features.Analytics.SearchAttempts.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Analytics.SearchAttempts.Controllers
{
    [ApiController]
    [Route("api/admin/attempts")]
    [Authorize(Roles ="Admin")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AnalyticsController(IMediator mediator)
            => _mediator = mediator;
        [HttpGet]
        public async Task<IActionResult> GetAttempts([FromQuery] GetAdminAttemptsViewModel filter, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(filter.ToDto(),cancellationToken));
    }
}
