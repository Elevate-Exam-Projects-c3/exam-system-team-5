using exam_system.Features.Common.Diplomas.DiplomaExists.Query;
using exam_system.Features.Diplomas.AdminUpdateDiploma.Commands;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Diplomas.AdminUpdateDiploma.Orchestrators
{


    public class UpdateDiplomaOrchestratorHandler : IRequestHandler<UpdateDiplomaOrchestrator, RequestResponse<Unit>>
    {
        private readonly IMediator _mediator;
        public UpdateDiplomaOrchestratorHandler(IMediator mediator) => _mediator = mediator;

        public async Task<RequestResponse<Unit>> Handle(UpdateDiplomaOrchestrator request, CancellationToken cancellationToken)
        {
            var diplomaExists = await _mediator.Send(new DiplomaExistsQuery(request.Id), cancellationToken);
            if (diplomaExists.Data != true)
                return RequestResponse<Unit>.Fail("Diploma not found.", 404);

            return await _mediator.Send(new UpdateDiplomaCommand(request.Id, request.Title, request.Description), cancellationToken);
        }
    }
}