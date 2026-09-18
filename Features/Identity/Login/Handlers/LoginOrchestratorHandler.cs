using exam_system.Common.Results;
using exam_system.Features.Identity.Login.Commands;
using exam_system.Features.Identity.Login.DTOs;
using exam_system.Features.Identity.Login.Orchestrators;
using exam_system.Features.Identity.Login.Queries;
using MediatR;

namespace exam_system.Features.Identity.Login.Handlers
{
    public class LoginOrchestratorHandler : IRequestHandler<LoginOrchestrator, Result<LoginResponseDto>>
    {
        private readonly IMediator _mediator;

        public LoginOrchestratorHandler(IMediator mediator)
           => _mediator = mediator;
        public async Task<Result<LoginResponseDto>> Handle(LoginOrchestrator request, CancellationToken cancellationToken)
        {
            var userResult = await _mediator.Send(new GetActiveUserByEmailQuery(request.Email), cancellationToken);
            if (userResult.IsFailure || userResult.Value is null)
                return Result<LoginResponseDto>.Failure("Invalid email or password");
            var user = userResult.Value;
            if (userResult.Value.FailedLoginAttempts >= 5)
                return Result<LoginResponseDto>.Failure("Account is locked due to too many failed attempts.");
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                await _mediator.Send(new IncrementFailedLoginAttemptsCommand(user.Id), cancellationToken);
                return Result<LoginResponseDto>.Failure("Invalid email or password");
            }
            if (user.FailedLoginAttempts > 0)
                await _mediator.Send(new ResetFailedLoginAttemptsCommand(user.Id), cancellationToken);

            var token = await _mediator.Send
               (new GenerateAuthTokensQuery(user.Id, user.Email, user.UserRole), cancellationToken);

            var saveToken = await _mediator.Send
               (new CreateRefreshTokenCommand(user.Id, token.RefreshToken, token.RefreshTokenExpiresAt), cancellationToken);
            if (saveToken.IsFailure)
            {
                return Result<LoginResponseDto>.Failure("Failed to save refresh token.");
            }
            var response = new LoginResponseDto(
               user.Id,
               user.FullName,
               user.Email,
               user.UserRole, 
               token.AccessToken,
               token.RefreshToken,
               token.RefreshTokenExpiresAt
   );

            return Result<LoginResponseDto>.Success(response);

        }
    }
}
