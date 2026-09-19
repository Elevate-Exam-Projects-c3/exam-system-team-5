using exam_system.Common.Results;
using exam_system.Features.Identity.Login.DTOs;
using MediatR;

namespace exam_system.Features.Identity.Login.Orchestrators
{
    public record LoginOrchestrator(string Email, string Password) : IRequest<Result<LoginResponseDto>>;
    
}
