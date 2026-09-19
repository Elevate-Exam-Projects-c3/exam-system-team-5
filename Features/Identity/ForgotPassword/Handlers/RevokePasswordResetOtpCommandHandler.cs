using exam_system.Common.Results;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.ForgotPassword.Commands;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Identity.ForgotPassword.Handlers
{
    public class RevokePasswordResetOtpCommandHandler : IRequestHandler<RevokePasswordResetOtpCommand, Result>
    {
        private readonly IGenericRepository<PasswordResetOtp> _otpRepository;

        public RevokePasswordResetOtpCommandHandler(IGenericRepository<PasswordResetOtp> otpRepository)
            => _otpRepository = otpRepository;
        public async Task<Result> Handle(RevokePasswordResetOtpCommand request, CancellationToken cancellationToken)
        {
            var record = await _otpRepository.Get(x => x.Email == request.Email && !x.IsDeleted&&!x.IsUsed)
                .OrderByDescending(x=>x.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);
            if (record is not null)
            {
                record.IsUsed = true;
                record.UpdatedAt = DateTime.UtcNow;
                await _otpRepository.UpdateAsync(record);
            }
            return Result.Success();
        }
    }
}
