using exam_system.Features.Identity.Logout.Commands;
using exam_system.Features.Identity.Logout.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Identity.Logout.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        => _mediator = mediator;
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogOutViewModel logoutVm)
        {
            var result = await _mediator.Send(new LogoutCommand(logoutVm.RefToken));
            return result.IsSuccess
                ? Ok(new { message = "Logged out successfully." })
                : BadRequest(new { error = result.ErrorMessage });
        }

    }
}
