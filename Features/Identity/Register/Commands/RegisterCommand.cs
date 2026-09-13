using exam_system.Common.Results;
using MediatR;

namespace exam_system.Features.Identity.Register.Commands
{
    public record RegisterCommand(
        Guid UserId,string FullName, 
        string Email,
        string Password):IRequest<Result>;
}
