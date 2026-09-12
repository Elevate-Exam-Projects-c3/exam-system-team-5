using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Common.Diplomas.DiplomaExists.Query
{
    public record DiplomaExistsQuery(Guid Id) : IRequest<RequestResponse<bool>>;
}
