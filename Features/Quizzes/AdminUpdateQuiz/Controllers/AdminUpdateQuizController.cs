using exam_system.Features.Quizzes.AdminCreateQuiz.Commands;
using exam_system.Features.Quizzes.AdminCreateQuiz.DTOS;
using exam_system.Features.Quizzes.AdminCreateQuiz.Orchestrators;
using exam_system.Features.Quizzes.AdminUpdateQuiz.Commands;
using exam_system.Features.Quizzes.AdminUpdateQuiz.DTOs;
using exam_system.Features.Quizzes.AdminUpdateQuiz.Handlers;
using exam_system.Features.Shared;
using exam_system.Features.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Quizzes.AdminUpdateQuiz.Controllers
{
    [ApiController]
    [Route("api/admin/quizzes{QuizId:guid}")]
    //[Authorize(Roles = "Admin")]
    public class AdminUpdateQuizController(UpdateQuizOrchestratorHandler updateQuizOrchestrator):ControllerBase
    {

        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] Guid QuizId, [FromBody] UpdateQuizRequestDto dto, CancellationToken cancellationToken = default)
        {
            var result = await updateQuizOrchestrator.Handle(new UpdateQuizCommand(QuizId, dto), cancellationToken);

            var response = EndpointResponse<QuizResponseDto>.FromResult(result);
            return Ok(response);
        }
    }
}
