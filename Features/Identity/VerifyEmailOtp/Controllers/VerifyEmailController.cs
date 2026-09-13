using exam_system.Features.Identity.VerifyEmailOtp.Orchestrators;
using exam_system.Features.Identity.ViewModels;
using exam_system.Features.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Identity.VerifyEmailOtp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyEmailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VerifyEmailController(IMediator mediator)
           => _mediator = mediator;

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyEmailOtpViewModel viewModel)
        {
            var result = await _mediator.Send(new VerifyEmailOrchestratorCommand(viewModel.Email, viewModel.OtpCode));
            return result.IsSuccess
                ? Ok("Account Active Successfully .")
                : BadRequest(ApiResponse.Fail(result.ErrorMessage));
        }
    }
}
