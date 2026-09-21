using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.GetStudentDashboard.Dtos;
using exam_system.Features.Diplomas.GetStudentDashboard.Queries;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Diplomas.GetStudentDashboard.Handlers
{
    public class GetEnrolledDiplomasQueryHandler : IRequestHandler<GetEnrolledDiplomasQuery, RequestResponse<List<EnrolledDiplomaSummaryDto>>>
    {
        private readonly IGenericRepository<StudentEnrollment> _enrollmentRepository;
        public GetEnrolledDiplomasQueryHandler(IGenericRepository<StudentEnrollment> enrollmentRepository)
            => _enrollmentRepository = enrollmentRepository;

        public async Task<RequestResponse<List<EnrolledDiplomaSummaryDto>>> Handle(GetEnrolledDiplomasQuery request, CancellationToken cancellationToken)
        {
            var diplomas = await _enrollmentRepository
                .Get(e => e.StudentId == request.StudentId)
                .ProjectToType<EnrolledDiplomaSummaryDto>()
                .ToListAsync(cancellationToken);

            return RequestResponse<List<EnrolledDiplomaSummaryDto>>.Ok(diplomas);
        }
    }
}
