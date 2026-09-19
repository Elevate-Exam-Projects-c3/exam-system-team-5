using exam_system.Common.Results;
using MediatR;

namespace exam_system.Features.Identity.ForgotPassword.Orchestrators
{
    public record ForgotPasswordOrchestratorCommand(string Email) : IRequest<Result>; 
}
