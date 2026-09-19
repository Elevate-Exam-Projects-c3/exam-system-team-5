using exam_system.Common.Results;
using MediatR;

namespace exam_system.Features.Identity.ForgotPassword.Commands
{
    public record CreatePasswordResetOtpCommand(Guid UserId, string Email, string FullName):IRequest<Result>;
    
}
