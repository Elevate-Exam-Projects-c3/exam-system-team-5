using exam_system.Features.Identity.Register.Commands;
using exam_system.Features.Identity.Register.DTOs;
using exam_system.Features.Shared;
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
            var result = await _mediator.Send
                (new RegisterCommand(request.FullName, request.Email, request.Password), cancellationToken);

            return result.IsSuccess
                ? StatusCode(StatusCodes.Status201Created,
                ApiResponse.Ok("Registration successful. Please check your email for the verification code."))
                : BadRequest(ApiResponse.Fail(result.ErrorMessage));
        }
    }
}
