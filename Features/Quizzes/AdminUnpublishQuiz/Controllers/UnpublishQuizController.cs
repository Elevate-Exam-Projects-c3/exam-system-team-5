using exam_system.Features.Quizzes.AdminUnpublishQuiz.Orchestrators;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Quizzes.AdminUnpublishQuiz.Controllers
{
    [Route("api/quizzes")]
    [ApiController]
    public class UnpublishQuizController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UnpublishQuizController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("/unpublish/{id}")]
        public async Task<IActionResult> Unpublish(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new UnpublishQuizOrchestrator(id),
                cancellationToken);

            return Ok(new
            {
                Success = true,
                Message = "Quiz unpublished successfully."
            });
        }
    }
}
