using exam_system.Common.Constants;
using exam_system.Features.Quizzes.AdminPublishQuiz.Commands;
using exam_system.Features.Quizzes.AdminPublishQuiz.Orchestrators;
using exam_system.Features.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Quizzes.AdminPublishQuiz.Controllers
{
    [Route("api/admin/quizzes")]
    [ApiController]
    [Authorize(Roles = AppRole.Admin)]
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
            var result = await _mediator.Send(
                new PublishQuizOrchestrator(id),
                cancellationToken);

            return StatusCode(
                result.StatusCode,
                EndpointResponse<Unit>.FromResult(result)
            );
        }
    }
}
