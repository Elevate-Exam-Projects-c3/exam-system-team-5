using exam_system.Features.Common.Diplomas.DiplomaActiveEnrollments.Query;
using exam_system.Features.Common.Diplomas.DiplomaExists.Query;
using exam_system.Features.Diplomas.AdminDeleteDiploma.Commands;
using exam_system.Features.Diplomas.AdminDeleteDiploma.Orchestrators;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Diplomas.AdminDeleteDiploma.Handlers
{
    public class DeleteDiplomaOrchestratorHandler : IRequestHandler<DeleteDiplomaOrchestrator, RequestResponse<Unit>>
    {
        private readonly IMediator _mediator;
        public DeleteDiplomaOrchestratorHandler(IMediator mediator) => _mediator = mediator;

        public async Task<RequestResponse<Unit>> Handle(DeleteDiplomaOrchestrator request, CancellationToken cancellationToken)
        {
            var diplomaExists = await _mediator.Send(new DiplomaExistsQuery(request.Id), cancellationToken);
            if (diplomaExists.Data != true)
                return RequestResponse<Unit>.Fail("Diploma not found.", 404);

            var hasActiveEnrollments = await _mediator.Send(new DiplomaHasActiveEnrollmentsQuery(request.Id), cancellationToken);
            if (hasActiveEnrollments.Data == true)
                return RequestResponse<Unit>.Fail("Cannot delete a diploma with active enrollments.", 409);

            return await _mediator.Send(new DeleteDiplomaCommand(request.Id), cancellationToken);
        }
    }
}
