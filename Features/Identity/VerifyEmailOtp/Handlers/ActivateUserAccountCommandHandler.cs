using exam_system.Common.Enums;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.VerifyEmailOtp.Commands;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Identity.VerifyEmailOtp.Handlers
{
    public class ActivateUserAccountCommandHandler : IRequestHandler<ActivateUserAccountCommand, Unit>
    {
        private readonly IGenericRepository<ApplicationUser> _userRepo;
        public ActivateUserAccountCommandHandler(IGenericRepository<ApplicationUser> userRepo,
            IGenericRepository<EmailVerificationOtp> otpRepo)
        => _userRepo = userRepo;
        public async Task<Unit> Handle(ActivateUserAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByIdAsync(request.UserId);
            if (user != null)
            {
                user.AccountStatus = AccountStatus.Active;
                user.EmailConfirmed = true;
                _userRepo.Update(user);
            }
            return Unit.Value;

        }
    }
}
