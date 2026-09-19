using exam_system.Common.Results;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.ForgotPassword.Commands;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Identity.ForgotPassword.Handlers
{
    public class VerifyPasswordResetOtpCommandHandler : IRequestHandler<VerifyPasswordResetOtpCommand, Result>
    {
        private readonly IGenericRepository<PasswordResetOtp> _otpRepository;

        public VerifyPasswordResetOtpCommandHandler(IGenericRepository<PasswordResetOtp> otpRepository)
            => _otpRepository = otpRepository;
        public async Task<Result> Handle(VerifyPasswordResetOtpCommand request, CancellationToken cancellationToken)
        {
            var record = await _otpRepository.Get(x =>
                x.Email == request.Email &&
                !x.IsUsed &&
                !x.IsDeleted)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);

            if (record is null)
                return Result.Failure("No OTP request found for this email.");

            if (DateTime.UtcNow > record.ExpiresAt)
                return Result.Failure("OTP has expired.");

            if (record.AttemptCount >= 5)
                return Result.Failure("Maximum attempts reached. Please request a new code.");

            bool isValid = BCrypt.Net.BCrypt.Verify(request.Otp, record.OtpHash);
            if (!isValid)
            {
                record.AttemptCount++;
                record.UpdatedAt = DateTime.UtcNow;
                await _otpRepository.UpdateAsync(record);

                int remaining = 5 - record.AttemptCount;
                return Result.Failure($"Invalid OTP. You have {remaining} attempts remaining.");
            }

            return Result.Success();
        }
    }
}
