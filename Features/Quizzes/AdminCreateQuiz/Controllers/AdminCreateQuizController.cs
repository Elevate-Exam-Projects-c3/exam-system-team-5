using exam_system.Features.Quizzes.AdminCreateQuiz.Commands;
using exam_system.Features.Quizzes.AdminCreateQuiz.Controllers.ViewModels;
using exam_system.Features.Quizzes.AdminCreateQuiz.Handlers;
using exam_system.Features.Quizzes.AdminCreateQuiz.Orchestrators;
using exam_system.Features.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Controllers
{
    [ApiController]
    [Route("api/admin/quizzes/{diplomaId:guid}")]
    //[Authorize(Roles = "Admin")]
    public class AdminCreateQuizController( IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromRoute] Guid diplomaId, [FromBody] CreateQuizRequestViewModel viewModel, CancellationToken cancellationToken = default)
        {
            var result = await mediator.Send(new CreateQuizOrchestrator(
                diplomaId,
                viewModel.Instructions,
                viewModel.Title,
                viewModel.DurationMinutes,
                viewModel.StartDate,    
                viewModel.EndDate,
                viewModel.PassScore,
                viewModel.MaxAttempts), cancellationToken);

            var response = EndpointResponse<bool>.FromResult(result);
            return StatusCode(response.StatusCode, response);
        }
    }
}
