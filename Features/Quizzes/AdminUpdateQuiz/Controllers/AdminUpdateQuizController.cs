using exam_system.Features.Quizzes.AdminCreateQuiz.Commands;
using exam_system.Features.Quizzes.AdminCreateQuiz.DTOS;
using exam_system.Features.Quizzes.AdminCreateQuiz.Orchestrators;
using exam_system.Features.Quizzes.AdminUpdateQuiz.Commands;
using exam_system.Features.Quizzes.AdminUpdateQuiz.Controllers.ViewModels;
using exam_system.Features.Quizzes.AdminUpdateQuiz.DTOs;
using exam_system.Features.Quizzes.AdminUpdateQuiz.Handlers;
using exam_system.Features.Shared;
using exam_system.Features.Shared.DTOs;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Quizzes.AdminUpdateQuiz.Controllers
{
    [ApiController]
    [Route("api/admin/quizzes/{QuizId:guid}")]
    //[Authorize(Roles = "Admin")]
    public class AdminUpdateQuizController(IMediator mediator) :ControllerBase
    {
        
        [HttpPut]
        public async Task<IActionResult> Update
            (
            [FromRoute] Guid QuizId,
            [FromBody] UpdateQuizRequestViewModel viewModel, 
            CancellationToken cancellationToken = default
            )
        {
            // Map the view model to the DTO
            var dto = new UpdateQuizRequestDto(
                Title: viewModel.Title,
                Instructions: viewModel.Instructions,
                DurationMinutes: viewModel.DurationMinutes,
                StartDate: viewModel.StartDate,
                EndDate: viewModel.EndDate,
                PassScore: viewModel.PassScore,
                MaxAttempts: viewModel.MaxAttempts
            );

            var result = await mediator.Send(new UpdateQuizCommand(QuizId, dto), cancellationToken);

            var response = EndpointResponse<bool>.FromResult(result);
            return StatusCode(response.StatusCode, response);
        }
    }
}
