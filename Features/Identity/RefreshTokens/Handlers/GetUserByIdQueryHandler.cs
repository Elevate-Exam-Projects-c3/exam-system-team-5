using exam_system.Common.Enums;
using exam_system.Common.Results;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.RefreshTokens.DTOs;
using exam_system.Features.Identity.RefreshTokens.Queries;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Identity.RefreshTokens.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserAuthDto>>
    {
        private readonly IGenericRepository<ApplicationUser> _userRepository;

        public GetUserByIdQueryHandler(IGenericRepository<ApplicationUser> userRepository)
           => _userRepository = userRepository;
        public async Task<Result<UserAuthDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user is null)
                return Result<UserAuthDto>.Failure("User not found.");
            if (user.AccountStatus != AccountStatus.Active)
                return Result<UserAuthDto>.Failure("User account is not active.");
            return Result<UserAuthDto>.Success(new UserAuthDto(
                user.Id,
                user.Email,
                user.Role,
                user.AccountStatus));  
        }
    }
}
