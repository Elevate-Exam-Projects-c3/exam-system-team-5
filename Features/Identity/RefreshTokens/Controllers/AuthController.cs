using exam_system.Features.Identity.RefreshTokens.Orchestrators;
using exam_system.Features.Identity.RefreshTokens.Queries;
using exam_system.Features.Identity.RefreshTokens.ViewModels;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Identity.RefreshTokens.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
            => _mediator = mediator;
        [HttpPost("refresh-token")]
        public async Task<IActionResult> GetRefreshToken(RefreshTokenViewModel refreshTokenViewModel)
        {
            var result = await _mediator.Send(new RefreshTokenOrchestratorCommand(refreshTokenViewModel.RefToken));
            return result.IsSuccess
            ? Ok(result.Value)
            : Unauthorized(new { error = result.ErrorMessage });

        }
    }
}
