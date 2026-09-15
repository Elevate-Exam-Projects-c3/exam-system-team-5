using exam_system.Features.Quizzes.AdminQuizPublishCheck.Orchestrators;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.Queries;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Publish_ReadinessController : ControllerBase
    {
        private readonly IMediator _mediator;

        public Publish_ReadinessController(IMediator mediator)
        {
            _mediator = mediator;
        }




        [HttpGet]
        [Route("api/quizzes/readiness/{quizId}")]
        public async Task<IActionResult> CheckReadiness(
            Guid quizId,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new QuizReadinessOrchestrator(quizId),
                cancellationToken);

            var model = new QuizReadinessViewModel
            {
                QuizId = result.QuizId,
                IsReady = result.IsReady,
                Checks = result.Checks
                    .Select(check => new ReadinessCheckViewModel
                    {
                        Name = check.Name,
                        Passed = check.Passed,
                        Message = check.Message
                    })
                    .ToList()
            };

            return Ok(model);
        }


    }
}
