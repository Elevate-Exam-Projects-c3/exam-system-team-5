using exam_system.Common.Results;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.Register.Commands;
using exam_system.Features.Identity.Register.Events;
using exam_system.Persistence.DataAccess;
using MediatR;
using System.Security.Cryptography;

namespace exam_system.Features.Identity.Register.Handlers
{
    public class CreateOtpCommandHandler : IRequestHandler<CreateOtpCommand, Result>
    {
        private readonly IGenericRepository<EmailVerificationOtp> _otpRepository;
        private readonly IMediator _mediator;

        public CreateOtpCommandHandler(IGenericRepository<EmailVerificationOtp> otpRepository, IMediator mediator)
        {

            _otpRepository = otpRepository;
            _mediator = mediator;
        }
        public async Task<Result> Handle(CreateOtpCommand request, CancellationToken cancellationToken)
        {
            string plainOtp = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();
            var otpHash = BCrypt.Net.BCrypt.HashPassword(plainOtp, workFactor: 12);
            var verificationOtp = new EmailVerificationOtp
            {
                UserId = request.UserId,
                Email = request.Email.Trim(),
                OtpHash = otpHash,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                AttemptCount = 0,
                IsUsed = false
            };
            await _otpRepository.AddAsync(verificationOtp);
            var notification = new UserRegisteredNotification(request.Email, request.FullName, plainOtp);
            await _mediator.Publish(notification, cancellationToken);
            return Result.Success();
        }
    }
}
