using exam_system.Common.Results;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.RefreshTokens.DTOs;
using exam_system.Features.Identity.RefreshTokens.Queries;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Identity.RefreshTokens.Handlers
{
    public class GetRefreshTokenByTokenQueryHandler : IRequestHandler<GetRefreshTokenByTokenQuery, Result<RefreshTokenDto>>
    {
        private readonly IGenericRepository<RefreshToken> _refrshTokenRepository;
        public GetRefreshTokenByTokenQueryHandler(IGenericRepository<RefreshToken> RefrshTokenRepository)
          => _refrshTokenRepository = RefrshTokenRepository;
        public async Task<Result<RefreshTokenDto>> Handle(GetRefreshTokenByTokenQuery request, CancellationToken cancellationToken)
        {
            var tokenDto = await _refrshTokenRepository.Get(rt => rt.Token == request.Token)
                .Select(rt => new RefreshTokenDto(
                      rt.Id,
                      rt.UserId,
                      rt.Token,
                      rt.ExpiresAt,
                      rt.IsUsed,
                      rt.IsRevoked
                    )).FirstOrDefaultAsync(cancellationToken);
            return tokenDto is null ? Result<RefreshTokenDto>.Failure("Refresh token not found")
                      : Result<RefreshTokenDto>.Success(tokenDto);

        }
    }
}
