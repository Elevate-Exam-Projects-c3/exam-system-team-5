using exam_system.Common.Results;
using MediatR;

namespace exam_system.Features.Identity.Register.Orchestrators
{
    public record RegisterOrchestrator( string FullName, string Email, string Password) : IRequest<Result>;
}
