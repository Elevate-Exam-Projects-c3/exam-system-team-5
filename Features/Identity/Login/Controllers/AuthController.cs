using exam_system.Features.Identity.Login.DTOs;
using exam_system.Features.Identity.Login.Orchestrators;
using exam_system.Features.Identity.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Identity.Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
           => _mediator = mediator;

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginViewModel LoginVm)
        {
            var result = await _mediator.Send(new LoginOrchestrator(LoginVm.Email, LoginVm.Password));
            return result.IsSuccess
                ? Ok(result.Value)
                : Unauthorized(new { message = result.ErrorMessage });
        }
    }
}
