using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Diplomas.AdminUpdateDiploma.Orchestrators
{
    public record UpdateDiplomaOrchestrator(Guid Id, string Title, string? Description) : IRequest<RequestResponse<Unit>>;
}
