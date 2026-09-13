using MediatR;

namespace exam_system.Features.Identity.VerifyEmailOtp.Commands
{
    public record ActivateUserAccountCommand(Guid UserId, Guid OtpId):IRequest<Unit>; 
    
}
