using exam_system.Common.Results;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.ForgotPassword.Commands;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Identity.ForgotPassword.Handlers
{
    public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand, Result>
    {
        private readonly IGenericRepository<ApplicationUser> _userRepository;

        public UpdateUserPasswordCommandHandler(IGenericRepository<ApplicationUser> userRepository)
            => _userRepository = userRepository;
        public async Task<Result> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(u => u.Id == request.UserId && !u.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);
            if (user is null)
                return Result.Failure("User not found.");
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword, workFactor: 12);
            user.UpdatedAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);
            return Result.Success();
        }
    }
}
