using exam_system.Common.Results;
using MediatR;

namespace exam_system.Features.Identity.Login.Commands
{
    public record CreateRefreshTokenCommand(Guid UserId, string Token, DateTime ExpiresAt) : IRequest<Result>;
}
