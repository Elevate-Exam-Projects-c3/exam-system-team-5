using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.VerifyEmailOtp.DTOs;
using exam_system.Features.Identity.VerifyEmailOtp.Queries;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Identity.VerifyEmailOtp.Handlers
{
    public class GetLatestOtpQueryHandler : IRequestHandler<GetLatestOtpQuery, OtpVerificationDto?>
    {
        private readonly IGenericRepository<EmailVerificationOtp> _otpRepo;

        public GetLatestOtpQueryHandler(IGenericRepository<EmailVerificationOtp> otpRepo)
        => _otpRepo = otpRepo;

        public async Task<OtpVerificationDto?> Handle(GetLatestOtpQuery request, CancellationToken cancellationToken)
        {
            var latestOtpDto = await _otpRepo.GetAll()
                .AsNoTracking()
                .Where(o => o.User.Email == request.Email)
                .OrderByDescending(o => o.CreatedAt)
                .Select(o => new OtpVerificationDto(o.Id,
                o.UserId,
                o.OtpHash,
                o.ExpiresAt,
                o.AttemptCount,
                o.IsUsed,
                o.User.AccountStatus))
                .FirstOrDefaultAsync(cancellationToken);
            return latestOtpDto;
        }
    }
}
