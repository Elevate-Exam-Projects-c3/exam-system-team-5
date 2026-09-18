using exam_system.Features.Common.Diplomas.DiplomaActiveEnrollments.Query;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace exam_system.Features.Common.Diplomas.DiplomaActiveEnrollments.Handler
{
    public class DiplomaHasActiveEnrollmentsQueryHandler: IRequestHandler<DiplomaHasActiveEnrollmentsQuery, RequestResponse<bool>>
    {
        private readonly IGenericRepository<Domain.Entities.Diplomas.StudentEnrollment> _enrollmentRepository;

        public DiplomaHasActiveEnrollmentsQueryHandler(IGenericRepository<Domain.Entities.Diplomas.StudentEnrollment> enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }
        public async Task<RequestResponse<bool>> Handle(DiplomaHasActiveEnrollmentsQuery query, CancellationToken cancellationToken)
        {
            var hasActiveEnrollments = await _enrollmentRepository.ExistsAsync(e => e.DiplomaId == query.DiplomaId, cancellationToken);

            return RequestResponse<bool>.Ok(hasActiveEnrollments);
        }
    }
}
