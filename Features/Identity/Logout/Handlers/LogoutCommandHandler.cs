using exam_system.Common.Results;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.Logout.Commands;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Identity.Logout.Handlers
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result<bool>>
    {
        private readonly IGenericRepository<RefreshToken> _tokenRepository;

        public LogoutCommandHandler(IGenericRepository<RefreshToken> tokenRepository)
            => _tokenRepository = tokenRepository;

        public async Task<Result<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var token =await _tokenRepository.Get(t => t.Token == request.RefToken).FirstOrDefaultAsync();
            if (token is null || token.IsRevoked)
                return Result<bool>.Success(true);
            token.IsRevoked = true;
            _tokenRepository.Update(token);
            return Result<bool>.Success(true);
        }
    }
}
