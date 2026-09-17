using exam_system.Common.Results;
using exam_system.Features.Identity.ForgotPassword.Commands;
using exam_system.Features.Identity.ForgotPassword.Orchestrators;
using exam_system.Features.Identity.ForgotPassword.Queries;
using MediatR;

namespace exam_system.Features.Identity.ForgotPassword.Handlers
{
    public class ResetPasswordOrchestratorCommandHandler : IRequestHandler<ResetPasswordOrchestratorCommand, Result>
    {
        private readonly IMediator _mediator;

        public ResetPasswordOrchestratorCommandHandler(IMediator mediator)
            => _mediator = mediator;
        public async Task<Result> Handle(ResetPasswordOrchestratorCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _mediator.Send(new GetUserByEmailQuery(request.Email), cancellationToken);
            if (userResult.IsFailure || userResult.Value is null)
                return Result.Failure(userResult.ErrorMessage ?? "User not found.");

            var verifyResult = await _mediator.Send(
                new VerifyPasswordResetOtpCommand(request.Email, request.Otp),
                cancellationToken);

            if (verifyResult.IsFailure)
                return verifyResult;

            var updatePasswordResult = await _mediator.Send(
                new UpdateUserPasswordCommand(userResult.Value.Id, request.NewPassword),
                cancellationToken);

            if (updatePasswordResult.IsFailure)
                return updatePasswordResult;

            return await _mediator.Send(
                new RevokePasswordResetOtpCommand(request.Email),
                cancellationToken);
        }
    }
}
