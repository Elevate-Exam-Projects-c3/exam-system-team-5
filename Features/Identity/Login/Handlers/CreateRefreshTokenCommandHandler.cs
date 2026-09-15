using exam_system.Common.Results;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.Login.Commands;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Identity.Login.Handlers
{
    public class CreateRefreshTokenCommandHandler : IRequestHandler<CreateRefreshTokenCommand, Result>
    {
        private readonly IGenericRepository<RefreshToken> _refreshTokenRepository;

        public CreateRefreshTokenCommandHandler(IGenericRepository<RefreshToken> refreshTokenRepository)
           => _refreshTokenRepository = refreshTokenRepository;
        public async Task<Result> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = new RefreshToken
            {
                UserId = request.UserId,
                Token = request.Token,
                ExpiresAt = request.ExpiresAt,
                IsUsed = false,
                IsRevoked = false
            };
           await _refreshTokenRepository.AddAsync(refreshToken);
            return Result.Success();
        }
    }
}
