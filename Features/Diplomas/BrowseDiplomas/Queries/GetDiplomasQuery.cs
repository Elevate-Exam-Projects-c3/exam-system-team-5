using exam_system.Features.Diplomas.BrowseDiplomas.Dtos;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Diplomas.BrowseDiplomas.Queries
{
    public record GetDiplomasQuery(Guid StudentId,int PageIndex = 1,int PageSize = 20) : IRequest<RequestResponse<PaginatedResult<DiplomaItemsResponse>>>;
}
