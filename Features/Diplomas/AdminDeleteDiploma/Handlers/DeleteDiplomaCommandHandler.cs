using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.AdminDeleteDiploma.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Diplomas.AdminDeleteDiploma.Handlers
{
    public class DeleteDiplomaCommandHandler : IRequestHandler<DeleteDiplomaCommand , RequestResponse<Unit>>
    {
        private readonly IGenericRepository<Diploma> _diplomaRepository;
        private readonly IGenericRepository<StudentEnrollment> _enrollmentRepository;
        public DeleteDiplomaCommandHandler(
            IGenericRepository<Diploma> diplomaRepository,
            IGenericRepository<StudentEnrollment> enrollmentRepository)
        {
            _diplomaRepository = diplomaRepository;
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<RequestResponse<Unit>> Handle(DeleteDiplomaCommand command, CancellationToken cancellationToken)
        {
            //
            var exists = await _diplomaRepository.ExistsAsync(d => d.Id == command.Id, cancellationToken);
            if (!exists)
                return RequestResponse<Unit>.Fail("Diploma not found.", 404);

            var hasActiveEnrollments = await _enrollmentRepository.ExistsAsync(e => e.DiplomaId == command.Id, cancellationToken);
            if (hasActiveEnrollments)
                return RequestResponse<Unit>.Fail("Cannot delete a diploma with active enrollments.", 409);

            //

            await _diplomaRepository.DeleteAsync(command.Id , cancellationToken);
            return RequestResponse<Unit>.Ok(Unit.Value, "Diploma deleted successfully.");


        }
    }
}
