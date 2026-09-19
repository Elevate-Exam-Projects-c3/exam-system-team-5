using exam_system.Common.Results;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.Login.Commands;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Identity.Login.Handlers
{
    public class ResetFailedLoginAttemptsCommandHandler : IRequestHandler<ResetFailedLoginAttemptsCommand, Result>
    {
        private readonly IGenericRepository<ApplicationUser> _userRepository;

        public ResetFailedLoginAttemptsCommandHandler(IGenericRepository<ApplicationUser> userRepository)
           => _userRepository = userRepository;
        public async Task<Result> Handle(ResetFailedLoginAttemptsCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user is null)
                return Result.Failure("User not found.");
            if (user.FailedLoginAttempts > 0 || user.LockoutEnd != null)
            {
                user.FailedLoginAttempts = 0;
                user.LockoutEnd = null;
                user.UpdatedAt = DateTime.UtcNow;
                await _userRepository.UpdateAsync(user);
            }
            return Result.Success();
        }
    }
}
