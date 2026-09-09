using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Common.Diplomas.DiplomaExists.Query;
using exam_system.Features.Diplomas.AdminUpdateDiploma.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Diplomas.AdminUpdateDiploma.Handlers
{
    public class UpdateDiplomaCommandHandler : IRequestHandler<UpdateDiplomaCommand , RequestResponse<Unit>>
    {
        private readonly IMediator _mediator;
        private readonly IGenericRepository<Diploma> _diplomaRepository;
        public UpdateDiplomaCommandHandler(IGenericRepository<Diploma> diplomaRepository , IMediator mediator) 
        { 
            _mediator = mediator;
            _diplomaRepository = diplomaRepository;

        }

        public async Task<RequestResponse<Unit>> Handle(UpdateDiplomaCommand command, CancellationToken cancellationToken)
        {
            //isexists query
            var diplomaExists = await _mediator.Send(new DiplomaExistsQuery(command.Id));
            if (!diplomaExists.Data)
                return RequestResponse<Unit>.Fail("Diploma not found.", 404);

            await _diplomaRepository.UpdateAsync(
                d => d.Id == command.Id,
                setters => setters
                    .SetProperty(d => d.Title, command.Title)
                    .SetProperty(d => d.Description, command.Description),cancellationToken);

            return RequestResponse<Unit>.Ok(Unit.Value, "Diploma updated successfully.");

        }
    }
    
}
