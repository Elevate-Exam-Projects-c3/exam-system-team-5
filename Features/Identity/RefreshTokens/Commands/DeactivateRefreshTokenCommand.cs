using exam_system.Common.Results;
using MediatR;

namespace exam_system.Features.Identity.RefreshTokens.Commands
{
    public record DeactivateRefreshTokenCommand(Guid RefreshTokenId, string ReplacedByToken) : IRequest<Result>;
}
