using exam_system.Common.Results;
using exam_system.Features.Identity.Register.Commands;
using exam_system.Features.Identity.Register.Orchestrators;
using exam_system.Features.Identity.Register.Queries;
using MediatR;

namespace exam_system.Features.Identity.Register.Handlers
{
    public class RegisterOrchestratorHandler : IRequestHandler<RegisterOrchestrator, Result>
    {
        private readonly IMediator _mediator;

        public RegisterOrchestratorHandler(IMediator mediator)
            => _mediator = mediator;
        public async Task<Result> Handle(RegisterOrchestrator request, CancellationToken cancellationToken)
        {
            var userId = Guid.NewGuid();
            var emailExists = await _mediator.Send(new CheckEmailExistsQuery(request.Email), cancellationToken);
            if (emailExists)
            {
                return Result.Failure("Email already exists.");
            }
            var registerResult = await _mediator.Send(new RegisterCommand(userId, request.FullName, request.Email, request.Password), cancellationToken);
            if (!registerResult.IsSuccess)
            {
                return Result.Failure("Failed to register the user");
            }
            var otpResult = await _mediator.Send(new CreateOtpCommand(userId, request.Email, request.FullName), cancellationToken);
            if (!otpResult.IsSuccess)
            {
                return Result.Failure("Failed to generate OTP.");
            }
            return Result.Success();
        }
    }
}
