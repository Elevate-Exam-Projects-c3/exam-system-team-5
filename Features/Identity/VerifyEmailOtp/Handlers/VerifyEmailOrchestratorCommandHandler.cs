using exam_system.Common.Enums;
using exam_system.Common.Results;
using exam_system.Features.Identity.VerifyEmailOtp.Commands;
using exam_system.Features.Identity.VerifyEmailOtp.Orchestrators;
using exam_system.Features.Identity.VerifyEmailOtp.Queries;
using MediatR;

namespace exam_system.Features.Identity.VerifyEmailOtp.Handlers
{
    public class VerifyEmailOrchestratorCommandHandler : IRequestHandler<VerifyEmailOrchestratorCommand, Result>
    {
        private readonly IMediator _mediator;

        public VerifyEmailOrchestratorCommandHandler(IMediator mediator)
            => _mediator = mediator;
        public async Task<Result> Handle(VerifyEmailOrchestratorCommand request, CancellationToken cancellationToken)
        {
            var otpData = await _mediator.Send(new GetLatestOtpQuery(request.Email));
            if (otpData == null)
                return Result.Failure("User not found or no OTP requested.");
            if (otpData.AccountStatus != AccountStatus.Pending)
                return Result.Failure("Account is already active or in an invalid state.");
            if (otpData.AttemptCount >= 5)
                return Result.Failure("OTP locked due to maximum failed attempts. Please request a new one.");
            if (otpData.ExpiresAt < DateTime.UtcNow)
                return Result.Failure("Code Expired");
            bool IsOtpVaild = BCrypt.Net.BCrypt.Verify(request.OtpCode, otpData.OtpHash);
            if (!IsOtpVaild)
            {
                await _mediator.Send(new RecoredFailedOtpAttemptCommand(otpData.OtpId), cancellationToken);
                return Result.Failure("Invalid Otp Code");
            }
            await _mediator.Send(new ActivateUserAccountCommand(otpData.UserId, otpData.OtpId), cancellationToken);
            return Result.Success();
        }
    }
}
