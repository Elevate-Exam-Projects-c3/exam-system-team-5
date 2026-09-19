using exam_system.Common.Results;
using MediatR;

namespace exam_system.Features.Identity.ForgotPassword.Commands
{
    public record UpdateUserPasswordCommand(Guid UserId, string NewPassword) : IRequest<Result>;
}
