using exam_system.Common.Results;
using MediatR;

namespace exam_system.Features.Identity.ForgotPassword.Orchestrators
{
    public record ResetPasswordOrchestratorCommand(string Email, string Otp, string NewPassword) : IRequest<Result>;
    
}
