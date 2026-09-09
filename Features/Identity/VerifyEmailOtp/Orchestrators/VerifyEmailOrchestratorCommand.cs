using exam_system.Common.Results;
using MediatR;

namespace exam_system.Features.Identity.VerifyEmailOtp.Orchestrators
{
    public record VerifyEmailOrchestratorCommand(string Email, string OtpCode) : IRequest<Result>;
}
