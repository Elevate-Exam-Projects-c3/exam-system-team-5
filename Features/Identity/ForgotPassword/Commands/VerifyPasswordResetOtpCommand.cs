using exam_system.Common.Results;
using MediatR;

namespace exam_system.Features.Identity.ForgotPassword.Commands
{
    public record VerifyPasswordResetOtpCommand(string Email, string Otp) : IRequest<Result>;
    
}   
