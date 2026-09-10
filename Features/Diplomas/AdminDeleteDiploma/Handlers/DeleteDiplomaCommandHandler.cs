using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Common.Diplomas.DiplomaActiveEnrollments.Query;
using exam_system.Features.Common.Diplomas.DiplomaExists.Query;
using exam_system.Features.Diplomas.AdminDeleteDiploma.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Diplomas.AdminDeleteDiploma.Handlers
{
    public class DeleteDiplomaCommandHandler : IRequestHandler<DeleteDiplomaCommand , RequestResponse<Unit>>
    {
        private readonly IGenericRepository<Diploma> _diplomaRepository;
        private readonly IMediator _mediator;
        public DeleteDiplomaCommandHandler(IGenericRepository<Diploma> diplomaRepository,IMediator mediator)
        {
            _diplomaRepository = diplomaRepository;
            _mediator = mediator;
        }

        public async Task<RequestResponse<Unit>> Handle(DeleteDiplomaCommand command, CancellationToken cancellationToken)
        {
            //can chaange the error code with enum of logic error with its message to be readavle to the consumers

            var diplomaExists = await _mediator.Send(new DiplomaExistsQuery(command.Id), cancellationToken);
            if (diplomaExists.Data != true)
                return RequestResponse<Unit>.Fail("Diploma not found.", 404);

            var hasActiveEnrollments = await _mediator.Send(new DiplomaHasActiveEnrollmentsQuery(command.Id), cancellationToken);
            if (hasActiveEnrollments.Data == true)
                return RequestResponse<Unit>.Fail("Cannot delete a diploma with active enrollments.", 409);

            await _diplomaRepository.DeleteAsync(command.Id, cancellationToken);
            return RequestResponse<Unit>.Ok(Unit.Value, "Diploma deleted successfully.");


        }
    }
}
