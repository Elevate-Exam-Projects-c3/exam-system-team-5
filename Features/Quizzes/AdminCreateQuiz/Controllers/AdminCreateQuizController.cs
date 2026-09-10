using exam_system.Features.Quizzes.AdminCreateQuiz.Commands;
using exam_system.Features.Quizzes.AdminCreateQuiz.Controllers.ViewModels;
using exam_system.Features.Quizzes.AdminCreateQuiz.DTOS;
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
            // Map the view model to the DTO
            var dto = new CreateQuizRequestDto(
                Title: viewModel.Title,
                Instructions: viewModel.Instructions,
                DurationMinutes: viewModel.DurationMinutes,
                StartDate: viewModel.StartDate,
                EndDate: viewModel.EndDate,
                PassScore: viewModel.PassScore,
                MaxAttempts: viewModel.MaxAttempts
            );

            var command = new CreateQuizCommand(diplomaId, dto);
            var result = await mediator.Send(new CreateQuizOrchestrator(command), cancellationToken);

            var response = EndpointResponse<bool>.FromResult(result);
            return StatusCode(response.StatusCode, response);
        }
    }
}
