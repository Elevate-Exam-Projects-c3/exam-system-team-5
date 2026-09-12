using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.VerifyEmailOtp.Commands;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Identity.VerifyEmailOtp.Handlers
{
    public class UpdateOtpStatusToUsedCommandHandler : IRequestHandler<UpdateOtpStatusToUsedCommand, Unit>
    {
        private readonly IGenericRepository<EmailVerificationOtp> _otpRepo;
        public UpdateOtpStatusToUsedCommandHandler(IGenericRepository<EmailVerificationOtp> otpRepo)
        => _otpRepo = otpRepo;
        public async Task<Unit> Handle(UpdateOtpStatusToUsedCommand request, CancellationToken cancellationToken)
        {
            var otp = await _otpRepo.GetByIdAsync(request.OtpId);
            if (otp != null)
            {
                otp.IsUsed = true;
                _otpRepo.Update(otp);
            }
            return Unit.Value;
        }
    }
}
