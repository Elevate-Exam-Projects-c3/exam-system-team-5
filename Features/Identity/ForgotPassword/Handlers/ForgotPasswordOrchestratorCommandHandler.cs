using exam_system.Common.Results;
using exam_system.Features.Identity.ForgotPassword.Commands;
using exam_system.Features.Identity.ForgotPassword.Orchestrators;
using exam_system.Features.Identity.ForgotPassword.Queries;
using MediatR;

namespace exam_system.Features.Identity.ForgotPassword.Handlers
{
    public class ForgotPasswordOrchestratorCommandHandler : IRequestHandler<ForgotPasswordOrchestratorCommand, Result>
    {
        private readonly IMediator _mediator;

        public ForgotPasswordOrchestratorCommandHandler(IMediator mediator)
            => _mediator = mediator;
        public async Task<Result> Handle(ForgotPasswordOrchestratorCommand request, CancellationToken cancellationToken)
        {

            var userResult = await _mediator.Send(new GetUserByEmailQuery(request.Email),cancellationToken);
            if (userResult.IsFailure || userResult.Value is null)
                return Result.Failure(userResult.ErrorMessage ?? "User Not Found");
            return await _mediator.Send(new CreatePasswordResetOtpCommand(
                userResult.Value.Id,
                userResult.Value.Email,
                userResult.Value.FullName
                ), cancellationToken);

        }
    }
}
