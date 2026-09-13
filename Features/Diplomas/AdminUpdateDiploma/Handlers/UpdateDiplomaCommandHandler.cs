using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.AdminUpdateDiploma.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;

public class UpdateDiplomaCommandHandler : IRequestHandler<UpdateDiplomaCommand, RequestResponse<Unit>>
{
    private readonly IGenericRepository<Diploma> _diplomaRepository;
    public UpdateDiplomaCommandHandler(IGenericRepository<Diploma> diplomaRepository)
        => _diplomaRepository = diplomaRepository;

    public async Task<RequestResponse<Unit>> Handle(UpdateDiplomaCommand command, CancellationToken cancellationToken)
    {
        await _diplomaRepository.UpdateAsync(
            d => d.Id == command.Id,
            setters => setters
                .SetProperty(d => d.Title, command.Title)
                .SetProperty(d => d.Description, command.Description),
            cancellationToken);

        return RequestResponse<Unit>.Ok(Unit.Value, "Diploma updated successfully.");
    }
}