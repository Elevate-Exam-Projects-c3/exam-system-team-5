using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Common.Diplomas.DiplomaExists.Query;
using exam_system.Features.Diplomas.EnrollDiploma.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Diplomas.EnrollDiploma.Orchestrators
{
    public class EnrollInDiplomaOrchestratorHandler : IRequestHandler<EnrollInDiplomaOrchestrator, RequestResponse<Unit>>
    {
        private readonly IMediator _mediator;
        private readonly IGenericRepository<StudentEnrollment> _enrollmentRepository;

        public EnrollInDiplomaOrchestratorHandler(IMediator mediator, IGenericRepository<StudentEnrollment> enrollmentRepository)
        {
            _mediator = mediator;
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<RequestResponse<Unit>> Handle(EnrollInDiplomaOrchestrator request, CancellationToken cancellationToken)
        {
            var diplomaExists = await _mediator.Send(new DiplomaExistsQuery(request.DiplomaId), cancellationToken);
            if (diplomaExists.Data != true)
                return RequestResponse<Unit>.Fail("Diploma not found.", 404);

            var alreadyEnrolled = await _enrollmentRepository.ExistsAsync(
                e => e.StudentId == request.StudentId && e.DiplomaId == request.DiplomaId, cancellationToken);
            if (alreadyEnrolled)
                return RequestResponse<Unit>.Fail("You are already enrolled in this diploma.", 409);

            return await _mediator.Send(new EnrollInDiplomaCommand(request.StudentId, request.DiplomaId), cancellationToken);
        }
    }
}