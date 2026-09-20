using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.AdminDeleteDiploma.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Diplomas.AdminDeleteDiploma.Handlers
{
    public class DeleteDiplomaCommandHandler : IRequestHandler<DeleteDiplomaCommand, RequestResponse<Unit>>
    {
        private readonly IGenericRepository<Diploma> _diplomaRepository;
        public DeleteDiplomaCommandHandler(IGenericRepository<Diploma> diplomaRepository)
            => _diplomaRepository = diplomaRepository;

        public async Task<RequestResponse<Unit>> Handle(DeleteDiplomaCommand command, CancellationToken cancellationToken)
        {
            await _diplomaRepository.DeleteAsync(command.Id, cancellationToken);
            return RequestResponse<Unit>.Ok(Unit.Value, "Diploma deleted successfully.");
        }
    }
}