using exam_system.Common.Enums;
using exam_system.Common.Results;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.Login.DTOs;
using exam_system.Features.Identity.Login.Queries;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Identity.Login.Handlers
{
    public class GetActiveUserByEmailQueryHandle : IRequestHandler<GetActiveUserByEmailQuery, Result<ActiveUserDto>>
    {
        private readonly IGenericRepository<ApplicationUser> _userRepository;

        public GetActiveUserByEmailQueryHandle(IGenericRepository<ApplicationUser> userRepository)
            => _userRepository = userRepository;
        public async Task<Result<ActiveUserDto>> Handle(GetActiveUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(u => u.Email == request.Email && !u.IsDeleted)
                 .FirstOrDefaultAsync(cancellationToken);
            if (user == null)
                return Result<ActiveUserDto>.Failure("User not found");
            if (!user.EmailConfirmed || user.AccountStatus != AccountStatus.Active)
                return Result<ActiveUserDto>.Failure("User is not active or email is not confirmed");
            if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.UtcNow)
                return Result<ActiveUserDto>.Failure("User is temporarily locked ");
            var userDto = new ActiveUserDto(
                user.Id,
                user.FullName ,
                user.Email,
                user.Role,
                user.PasswordHash, 
                user.FailedLoginAttempts);
            return Result<ActiveUserDto>.Success(userDto);
        }
    }
}

