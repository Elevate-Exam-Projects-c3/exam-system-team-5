using exam_system.Features.Quizzes.AdminPublishQuiz.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Quizzes.AdminPublishQuiz.Controllers
{
    [Route("api/admin/quizzes")]
    [ApiController]
    public class PublishQuizController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PublishQuizController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("{id}/publish")]
        public async Task<IActionResult> Publish(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new PublishQuizOrchestrator(id),
                cancellationToken);

            return Ok(new
            {
                Success = true,
                Message = "Quiz published successfully."
            });
        }
    }
}
