using exam_system.Common.Results;
using MediatR;

namespace exam_system.Features.Identity.Logout.Commands
{
    public record LogoutCommand(string RefToken) : IRequest<Result<bool>>;
    
}
