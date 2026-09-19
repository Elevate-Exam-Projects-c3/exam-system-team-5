using exam_system.Features.Common.Diplomas.DiplomaActiveEnrollments.Query;
using exam_system.Features.Common.Diplomas.DiplomaExists.Query;
using exam_system.Features.Diplomas.AdminDeleteDiploma.Commands;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Diplomas.AdminDeleteDiploma.Orchestrators
{
    public record DeleteDiplomaOrchestrator(Guid Id) : IRequest<RequestResponse<Unit>>;

}