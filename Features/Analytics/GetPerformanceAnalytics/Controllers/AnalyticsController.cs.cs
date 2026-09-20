using exam_system.Features.Analytics.GetPerformanceAnalytics.Queries;
using exam_system.Features.Analytics.GetPerformanceAnalytics.ViewModels;
using exam_system.Features.Shared;
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
        public async Task<IActionResult> GetQuizPassRate(
            [FromQuery] AnalyticsFilterViewModel filterVm
            , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                 new GetQuizPassRateQuery(filterVm.DateFrom, filterVm.DateTo, filterVm.DiplomaId), cancellationToken);
            var response = result.Select(d => new QuizPassRateViewModel(
            d.QuizId,
            d.QuizTitle,
            d.TotalAttempts,
            d.PassedAttempts,
            d.PassRatePercentage)).ToList();
            return Ok(ApiResponse.Ok(response));
        }
        [HttpGet("avg-score-by-diploma")]
        public async Task<IActionResult> GetDiplomaAvgScore(
             [FromQuery] AnalyticsFilterViewModel filterVm,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetDiplomaAvgScoreQuery(filterVm.DateFrom, filterVm.DateTo, filterVm.DiplomaId),
                cancellationToken);

            var response = result.Select(d => new DiplomaAvgScoreViewModel(
                d.DiplomaId,
                d.DiplomaTitle,
                d.TotalAttempts,
                d.AverageScore)).ToList();

            return Ok(ApiResponse.Ok(response));
        }
        [HttpGet("attempts-over-time")]
        public async Task<IActionResult> GetAttemptsOverTime([FromQuery] AnalyticsFilterViewModel filterVm,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetAttemptsOverTimeQuery(filterVm.DateFrom, filterVm.DateTo, filterVm.DiplomaId),
                cancellationToken);

            var response = result.Select(d => new AttemptsOverTimeViewModel(
                d.Date,
                d.AttemptCount)).ToList();

            return Ok(ApiResponse.Ok(response));
        }

        [HttpGet("top-failed-questions")]
        public async Task<IActionResult> GetTopFailedQuestions(
              [FromQuery] AnalyticsFilterViewModel filterVm,
              [FromQuery] int take = 10,
              [FromQuery] int minAnswers = 1,
              [FromQuery] double maxSuccessRate = 40.0,
              CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(
                new GetTopFailedQuestionsQuery(
                    filterVm.DateFrom,
                    filterVm.DateTo,
                    filterVm.DiplomaId,
                    take,
                    minAnswers,
                    maxSuccessRate),
                cancellationToken);

            var response = result.Select(d => new TopFailedQuestionViewModel(
                d.QuestionId,
                d.QuestionText,
                d.QuizTitle,
                d.TotalAnswers,
                d.IncorrectAnswers,
                d.FailureRatePercentage)).ToList();
            return Ok(ApiResponse.Ok(response));
        }

    }
}
