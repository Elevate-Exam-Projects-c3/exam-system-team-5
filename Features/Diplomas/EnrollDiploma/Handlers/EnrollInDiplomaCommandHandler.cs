using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Common.Diplomas.DiplomaExists.Query;
using exam_system.Features.Diplomas.EnrollDiploma.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.Context;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace exam_system.Features.Diplomas.EnrollDiploma.Handlers
{
    public class EnrollInDiplomaCommandHandler : IRequestHandler<EnrollInDiplomaCommand, RequestResponse<Unit>>
    {
        private readonly IMediator _mediator;
        private readonly IGenericRepository<StudentEnrollment> _enrollmentRepository;
        public EnrollInDiplomaCommandHandler(IMediator mediator,IGenericRepository<StudentEnrollment> enrollmentRepository)
        {
            _mediator = mediator;
            _enrollmentRepository = enrollmentRepository;
        }
        public async Task<RequestResponse<Unit>> Handle(EnrollInDiplomaCommand command, CancellationToken cancellationToken)
        {
            var diplomaExists = await _mediator.Send(new DiplomaExistsQuery(command.DiplomaId), cancellationToken);
            if (diplomaExists.Data != true)
                return RequestResponse<Unit>.Fail("Diploma not found.", 404);

            var alreadyEnrolled = await _enrollmentRepository.ExistsAsync
                (e => e.StudentId == command.StudentId && e.DiplomaId == command.DiplomaId, cancellationToken);

            if (alreadyEnrolled)
                return RequestResponse<Unit>.Fail("You are already enrolled in this diploma.", 409);

            var enrollment = new StudentEnrollment
            {
                StudentId = command.StudentId,
                DiplomaId = command.DiplomaId,
                EnrolledAt = DateTime.UtcNow
            };
                _enrollmentRepository.Add(enrollment);

            return RequestResponse<Unit>.Ok(Unit.Value, "Enrolled successfully.");

        }
    }
}
