using exam_system.Common.Results;
using exam_system.Features.Identity.Login.DTOs;
using MediatR;

namespace exam_system.Features.Identity.Login.Queries
{
    public record GetActiveUserByEmailQuery(string Email): IRequest<Result<ActiveUserDto>>;
}
