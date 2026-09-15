using exam_system.Common.Results;
using exam_system.Features.Identity.RefreshTokens.DTOs;
using MediatR;

namespace exam_system.Features.Identity.RefreshTokens.Queries
{
    public record GetRefreshTokenByTokenQuery(string Token) : IRequest<Result<RefreshTokenDto>>;
}
