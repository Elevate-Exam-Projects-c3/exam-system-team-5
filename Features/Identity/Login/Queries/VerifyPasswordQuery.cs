using exam_system.Common.Results;
using MediatR;

namespace exam_system.Features.Identity.Login.Queries
{
    public record VerifyPasswordQuery(string ProviderPassword, string HashedPassword) : IRequest<Result<bool>>;
}
