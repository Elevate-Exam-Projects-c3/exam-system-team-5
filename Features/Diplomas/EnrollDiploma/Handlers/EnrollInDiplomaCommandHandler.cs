using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.EnrollDiploma.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Diplomas.EnrollDiploma.Handlers
{
    public class EnrollInDiplomaCommandHandler : IRequestHandler<EnrollInDiplomaCommand, RequestResponse<Unit>>
    {
        private readonly IGenericRepository<StudentEnrollment> _enrollmentRepository;
        public EnrollInDiplomaCommandHandler(IGenericRepository<StudentEnrollment> enrollmentRepository)
            => _enrollmentRepository = enrollmentRepository;

        public Task<RequestResponse<Unit>> Handle(EnrollInDiplomaCommand command, CancellationToken cancellationToken)
        {
            var enrollment = new StudentEnrollment
            {
                StudentId = command.StudentId,
                DiplomaId = command.DiplomaId,
                EnrolledAt = DateTime.UtcNow
            };
            _enrollmentRepository.Add(enrollment);

            return Task.FromResult(RequestResponse<Unit>.Ok(Unit.Value, "Enrolled successfully."));
        }
    }
}