using exam_system.Common.Results;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.ForgotPassword.Dtos;
using exam_system.Features.Identity.ForgotPassword.Queries;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Identity.ForgotPassword.Handlers
{
    public class GetOtpByResetTokenQueryHandler : IRequestHandler<GetOtpByResetTokenQuery, Result<ResetPasswordOtpDto>>
    {
        private readonly IGenericRepository<PasswordResetOtp> _otpRepository;

        public GetOtpByResetTokenQueryHandler(IGenericRepository<PasswordResetOtp> otpRepository)
            => _otpRepository = otpRepository;
        public async Task<Result<ResetPasswordOtpDto>> Handle(GetOtpByResetTokenQuery request, CancellationToken cancellationToken)
        {
            var recored = await _otpRepository.Get(x =>
             x.Email == request.Email &&
             x.ResetToken == request.ResetToken &&
             !x.IsUsed &&
             !x.IsDeleted)
             .AsNoTracking()
             .OrderByDescending(x => x.CreatedAt)
             .Select(x => new ResetPasswordOtpDto(x.Id, x.UserId, x.ResetTokenExpiresAt))
             .FirstOrDefaultAsync(cancellationToken);
            return recored is null
               ? Result<ResetPasswordOtpDto>.Failure("Invalid Or Already Used ResetToken")
               : Result<ResetPasswordOtpDto>.Success(recored);
        }
    }
}
