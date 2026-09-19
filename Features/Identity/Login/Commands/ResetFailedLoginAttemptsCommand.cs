using exam_system.Common.Results;
using MediatR;

namespace exam_system.Features.Identity.Login.Commands
{
    public record ResetFailedLoginAttemptsCommand(Guid UserId):IRequest<Result>; 
}
