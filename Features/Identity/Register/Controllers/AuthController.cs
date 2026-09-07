using exam_system.DTOs;
using exam_system.Features.Identity.Register.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Identity.Register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request, CancellationToken cancellationToken)
        {
           
            var command = new RegisterCommand(request.FullName, request.Email, request.Password);
            var userId = await _mediator.Send(command, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, new
            {
                UserId = userId,
                Message = "Registration successful. Please check your email for the verification code."
            });
        }
    }
}
