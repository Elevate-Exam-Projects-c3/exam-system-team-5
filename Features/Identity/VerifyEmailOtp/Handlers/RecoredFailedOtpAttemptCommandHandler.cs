using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.VerifyEmailOtp.Commands;
using exam_system.Persistence.DataAccess;
using MediatR;
using System.Reflection.Emit;

namespace exam_system.Features.Identity.VerifyEmailOtp.Handlers
{
    public class RecoredFailedOtpAttemptCommandHandler : IRequestHandler<RecoredFailedOtpAttemptCommand>
    {
        private readonly IGenericRepository<EmailVerificationOtp> _otpRepo;

        public RecoredFailedOtpAttemptCommandHandler(IGenericRepository<EmailVerificationOtp> otpRepo)
        {
            _otpRepo = otpRepo;
        }
        public async Task Handle(RecoredFailedOtpAttemptCommand request, CancellationToken cancellationToken)
        {
            var otp = await _otpRepo.GetByIdAsync(request.OtpId);
            if (otp != null)
            {
                otp.AttemptCount += 1;
                _otpRepo.Update(otp);
            }

        }
    }
}
