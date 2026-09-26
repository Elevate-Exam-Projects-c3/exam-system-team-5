using exam_system.Common.Constants;
using exam_system.Features.Quizzes.AdminDeleteQuiz.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Quizzes.AdminDeleteQuiz.Controllers
{
    [ApiController]
    [Route("api/admin/quizzes/{QuizId}")]
    [Authorize(Policy = AppPolicies.RequireAdmin)]
    public class DeleteQuizController(IMediator mediator):ControllerBase
    {
        [HttpDelete]
        public async Task<IActionResult> DeleteQuiz([FromRoute] Guid QuizId)
        {

            var result = await mediator.Send(new DeleteQuizCommand(QuizId));
            if (!result.Success)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }
    }
}
