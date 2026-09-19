using exam_system.Features.Analytics.GetPerformanceAnalytics.Queries;
using exam_system.Features.Analytics.GetPerformanceAnalytics.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Analytics.GetPerformanceAnalytics.Controllers
{
    [ApiController]
    [Route("api/admin/analytics")]
    [Authorize(Roles = "Admin")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AnalyticsController(IMediator mediator)
           => _mediator = mediator;

        [HttpGet("pass-rate-by-quiz")]
        public async Task<IActionResult> GetQuizPassRate([FromQuery] AnalyticsFilterViewModel filterVm,CancellationToken cancellationToken)
            => Ok(await _mediator.Send(new GetQuizPassRateQuery(filterVm.ToDto()),cancellationToken));

        [HttpGet("avg-score-by-diploma")]
        public async Task<IActionResult> GetDiplomaAvgScore([FromQuery] AnalyticsFilterViewModel filterVm,CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetDiplomaAvgScoreQuery(filterVm.ToDto()),cancellationToken));

        [HttpGet("attempts-over-time")]
        public async Task<IActionResult> GetAttemptsOverTime([FromQuery] AnalyticsFilterViewModel filterVm,CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetAttemptsOverTimeQuery(filterVm.ToDto()),cancellationToken));

        [HttpGet("top-failed-questions")]
        public async Task<IActionResult> GetTopFailedQuestions(
        [FromQuery] AnalyticsFilterViewModel filterVm,
        [FromQuery] int take = 10,
        [FromQuery] int minAnswers = 1,
        [FromQuery] double maxSuccessRate = 40.0,
        CancellationToken cancellationToken = default)
        => Ok(await _mediator.Send(
            new GetTopFailedQuestionsQuery(
                filterVm.ToDto(),
                take,
                minAnswers,
                maxSuccessRate),
            cancellationToken));

    }
}
