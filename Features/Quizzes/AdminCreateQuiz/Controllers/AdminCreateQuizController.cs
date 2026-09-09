using exam_system.Features.Quizzes.AdminCreateQuiz.Commands;
using exam_system.Features.Quizzes.AdminCreateQuiz.DTOS;
using exam_system.Features.Quizzes.AdminCreateQuiz.Handlers;
using exam_system.Features.Quizzes.AdminCreateQuiz.Orchestrators;
using exam_system.Features.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Controllers
{
    [ApiController]
    [Route("api/admin/quizzes{diplomaId:guid}")]
    //[Authorize(Roles = "Admin")]
    public class AdminCreateQuizController(CreateQuizOrchestratorHandler createQuizOrchestrator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromRoute] Guid diplomaId, [FromBody] CreateQuizRequestDto dto, CancellationToken cancellationToken = default)
        {
            var result = await createQuizOrchestrator.Handle(new CreateQuizCommand(diplomaId, dto), cancellationToken);

            var response = EndpointResponse<bool>.FromResult(result);
            return Ok(response);
        }
    }
}
