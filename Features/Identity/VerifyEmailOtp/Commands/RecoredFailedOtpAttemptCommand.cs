using MediatR;

namespace exam_system.Features.Identity.VerifyEmailOtp.Commands
{
    public record RecoredFailedOtpAttemptCommand(Guid OtpId) : IRequest;
}
