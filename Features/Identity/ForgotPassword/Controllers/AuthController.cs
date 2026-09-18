using exam_system.Features.Identity.ForgotPassword.Orchestrators;
using exam_system.Features.Identity.ForgotPassword.ViewModels;
using exam_system.Features.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Identity.ForgotPassword.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgetPasswordViewModel forgetPasswordVm, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new ForgotPasswordOrchestratorCommand(forgetPasswordVm.Email),
                cancellationToken);

            return result.IsSuccess
                ? Ok("Verification code sent to your email.")
                : BadRequest(ApiResponse.Fail(result.ErrorMessage));
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel resetPasswordViewModel , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new ResetPasswordOrchestratorCommand(resetPasswordViewModel.Email, resetPasswordViewModel.Otp, resetPasswordViewModel.NewPassword),
                cancellationToken);

            return result.IsSuccess
                ? Ok("Password has been reset successfully.")
                : BadRequest(ApiResponse.Fail(result.ErrorMessage));
        }
    }
}
