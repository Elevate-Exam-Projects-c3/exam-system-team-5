using exam_system.Common.Enums;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.VerifyEmailOtp.Commands;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Identity.VerifyEmailOtp.Handlers
{
    public class ActivateUserAccountCommandHandler : IRequestHandler<ActivateUserAccountCommand>
    {
        private readonly IGenericRepository<ApplicationUser> _userRepo;
        private readonly IGenericRepository<EmailVerificationOtp> _otpRepo;

        public ActivateUserAccountCommandHandler(IGenericRepository<ApplicationUser> userRepo,
            IGenericRepository<EmailVerificationOtp> otpRepo)
        {
            _userRepo = userRepo;
            _otpRepo = otpRepo;
        }
        public async Task Handle(ActivateUserAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByIdAsync(request.UserId);
            var otp = await _otpRepo.GetByIdAsync(request.OtpId);
            if(user !=null && otp !=null)
            {
                user.AccountStatus = AccountStatus.Active;
                user.EmailConfirmed = true;
                otp.IsUsed = true;
                _userRepo.Update(user);
                _otpRepo.Update(otp);
            }

        }
    }
}
