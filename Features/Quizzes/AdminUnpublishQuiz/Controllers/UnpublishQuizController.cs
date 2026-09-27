using exam_system.Features.Quizzes.AdminUnpublishQuiz.Orchestrators;
using exam_system.Features.Shared;
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

            [HttpPatch("/{id}/unpublish")]
            public async Task<IActionResult> Unpublish(
                Guid id,
                CancellationToken cancellationToken)
            {
                var result = await _mediator.Send(
                    new UnpublishQuizOrchestrator(id),
                    cancellationToken);

                return StatusCode(
                    result.StatusCode,
                    EndpointResponse<Unit>.FromResult(result)
                );
            }
        }
    }


