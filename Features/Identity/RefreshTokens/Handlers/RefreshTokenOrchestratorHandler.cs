using exam_system.Common.Results;
using exam_system.Features.Identity.Login.Commands;
using exam_system.Features.Identity.Login.DTOs;
using exam_system.Features.Identity.Login.Queries;
using exam_system.Features.Identity.RefreshTokens.Commands;
using exam_system.Features.Identity.RefreshTokens.DTOs;
using exam_system.Features.Identity.RefreshTokens.Orchestrators;
using exam_system.Features.Identity.RefreshTokens.Queries;
using MediatR;

namespace exam_system.Features.Identity.RefreshTokens.Handlers
{
    public class RefreshTokenOrchestratorHandler : IRequestHandler<RefreshTokenOrchestratorCommand, Result<AuthTokenDto>>
    {
        private readonly IMediator _mediator;

        public RefreshTokenOrchestratorHandler(IMediator mediator)
            => _mediator = mediator;
        public async Task<Result<AuthTokenDto>> Handle(RefreshTokenOrchestratorCommand request, CancellationToken cancellationToken)
        {
            var tokenResult = await _mediator.Send(new GetRefreshTokenByTokenQuery(request.RefToken),cancellationToken);
            if (!tokenResult.IsSuccess)
                return Result<AuthTokenDto>.Failure(tokenResult.ErrorMessage);
            var tokenDto = tokenResult.Value;
            var validateError = tokenDto switch
            {
                { IsUsed: true } => "Refresh Token has Already been used",
                { IsRevoked: true } => "Refresh Token has been revoked",
                _ when tokenDto!.ExpiresAt < DateTime.UtcNow => "Refresh Token has expired",
                _ => null
            };
            if (validateError != null)
                return Result<AuthTokenDto>.Failure(validateError);
            // GetUser and Chech Accout IsActive 
            var userResult = await _mediator.Send(new GetUserByIdQuery(tokenDto.UserId),cancellationToken);
            if (!userResult.IsSuccess)
                return Result<AuthTokenDto>.Failure(userResult.ErrorMessage);
            var userDto = userResult.Value;
            // Generate RefreshToken For User 
            var newAuthToken = await _mediator.Send(new GenerateAuthTokensQuery(
                userDto!.Id,
                userDto.Email,
                userDto.Role), cancellationToken);
            // Stop Old Token 
            var deactivateResult = await _mediator.Send(new DeactivateRefreshTokenCommand(
                tokenDto.Id,
                newAuthToken.RefreshToken),cancellationToken);
            if (!deactivateResult.IsSuccess)
                return Result<AuthTokenDto>.Failure(deactivateResult.ErrorMessage);
            // Create a new refresh token for the user
            var createTokenResult = await _mediator.Send(new CreateRefreshTokenCommand(
                userDto.Id,
                newAuthToken.RefreshToken,
                newAuthToken.RefreshTokenExpiresAt),cancellationToken);
            if (!createTokenResult.IsSuccess)
                return Result<AuthTokenDto>.Failure(createTokenResult.ErrorMessage);
            return Result<AuthTokenDto>.Success(newAuthToken);
        }
    }
}
