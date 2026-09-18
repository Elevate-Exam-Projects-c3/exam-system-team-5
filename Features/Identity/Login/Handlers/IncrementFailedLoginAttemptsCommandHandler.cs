using exam_system.Common.Results;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.Login.Commands;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Identity.Login.Handlers
{
    public class IncrementFailedLoginAttemptsCommandHandler : IRequestHandler<IncrementFailedLoginAttemptsCommand, Result>
    {
        private readonly IGenericRepository<ApplicationUser> _userRepository;

        public IncrementFailedLoginAttemptsCommandHandler(IGenericRepository<ApplicationUser> userRepository)
            => _userRepository = userRepository;
        public async Task<Result> Handle(IncrementFailedLoginAttemptsCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user is null)
                return Result.Failure("User not found.");
            user.FailedLoginAttempts++;
            return Result.Success();
        }
    }
}
