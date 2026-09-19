using exam_system.Common.Results;
using exam_system.Features.Identity.Login.DTOs;
using exam_system.Features.Identity.RefreshTokens.DTOs;
using MediatR;

namespace exam_system.Features.Identity.RefreshTokens.Orchestrators
{
    public record RefreshTokenOrchestratorCommand(string RefToken) : IRequest<Result<AuthTokenDto>>;
}
