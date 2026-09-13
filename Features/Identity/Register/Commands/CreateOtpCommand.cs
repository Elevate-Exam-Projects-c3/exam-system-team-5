using exam_system.Common.Results;
using MediatR;

namespace exam_system.Features.Identity.Register.Commands
{
    public record CreateOtpCommand(Guid UserId,
        string Email,
        string FullName):IRequest<Result>;
}
