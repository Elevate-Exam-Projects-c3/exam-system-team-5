using exam_system.Common.Results;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.RefreshTokens.Commands;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Identity.RefreshTokens.Handlers
{
    public class DeactivateRefreshTokenCommandHandler : IRequestHandler<DeactivateRefreshTokenCommand, Result>
    {
        private readonly IGenericRepository<RefreshToken> _refreshTokenRepository;

        public DeactivateRefreshTokenCommandHandler(IGenericRepository<RefreshToken> refreshTokenRepository)
          => _refreshTokenRepository = refreshTokenRepository;
        public async Task<Result> Handle(DeactivateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var token = await _refreshTokenRepository.GetByIdAsync(request.RefreshTokenId);
            if (token is null)
                return Result.Failure("Refresh Token Not Found");
            token.IsUsed = true;
            token.ReplacedByToken = request.ReplacedByToken;
            _refreshTokenRepository.Update(token);
            return Result.Success();
        }
    }
}
