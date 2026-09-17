using exam_system.Common.Results;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.ForgotPassword.Commands;
using exam_system.Features.Identity.ForgotPassword.Events;
using exam_system.Persistence.DataAccess;
using MediatR;
using System.Security.Cryptography;

namespace exam_system.Features.Identity.ForgotPassword.Handlers
{
    public class CreatePasswordResetOtpCommandHandler : IRequestHandler<CreatePasswordResetOtpCommand, Result>
    {
        private readonly IGenericRepository<PasswordResetOtp> _otpRepository;
        private readonly IMediator _mediator;

        public CreatePasswordResetOtpCommandHandler(IGenericRepository<PasswordResetOtp> otpRepository, IMediator mediator)
        {
            this._mediator = mediator;
            _otpRepository = otpRepository;
        }

        public async Task<Result> Handle(CreatePasswordResetOtpCommand request, CancellationToken cancellationToken)
        {
            var plainOtp = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();
            var otpHash = BCrypt.Net.BCrypt.HashPassword(plainOtp, workFactor: 12);
            var resetOto = new PasswordResetOtp
            {
                UserId = request.UserId,
                Email = request.Email,
                OtpHash = otpHash,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                AttemptCount = 0,
                IsUsed = false,
                CreatedAt=DateTime.UtcNow
                
            };
            await _otpRepository.AddAsync(resetOto);

            var notification = new PasswordResetOtpCreatedNotification(
                request.Email.Trim(),
                request.FullName.Trim(),
                plainOtp);
            await _mediator.Publish(notification, cancellationToken);
            return Result.Success();
        }
    }
}   
