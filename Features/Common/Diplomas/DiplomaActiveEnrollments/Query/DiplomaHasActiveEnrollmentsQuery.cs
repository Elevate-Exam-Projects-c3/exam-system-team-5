using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Common.Diplomas.DiplomaActiveEnrollments.Query
{
    public record DiplomaHasActiveEnrollmentsQuery(Guid DiplomaId) : IRequest<RequestResponse<bool>>;

}
