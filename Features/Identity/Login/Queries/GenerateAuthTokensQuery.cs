using exam_system.Common.Enums;
using exam_system.Features.Identity.Login.DTOs;
using MediatR;

namespace exam_system.Features.Identity.Login.Queries
{
    public record GenerateAuthTokensQuery(Guid UserId, string email, UserRole UserRole):IRequest<AuthTokenDto>;
}
