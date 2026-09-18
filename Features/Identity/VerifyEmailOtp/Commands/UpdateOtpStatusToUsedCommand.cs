using MediatR;

namespace exam_system.Features.Identity.VerifyEmailOtp.Commands
{
    public record UpdateOtpStatusToUsedCommand(Guid OtpId) : IRequest<Unit>;
}
