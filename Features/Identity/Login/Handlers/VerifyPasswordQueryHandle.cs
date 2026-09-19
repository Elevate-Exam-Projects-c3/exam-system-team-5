using exam_system.Common.Results;
using exam_system.Features.Identity.Login.Queries;
using MediatR;

namespace exam_system.Features.Identity.Login.Handlers
{
    public class VerifyPasswordQueryHandle : IRequestHandler<VerifyPasswordQuery, Result<bool>>
    {
        public Task<Result<bool>> Handle(VerifyPasswordQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.ProviderPassword) || string.IsNullOrWhiteSpace(request.HashedPassword))
                return Task.FromResult(Result<bool>.Failure("Invalid Password"));

            var isValid = BCrypt.Net.BCrypt.Verify(
                request.ProviderPassword,
                request.HashedPassword);
            return Task.FromResult(
                isValid
                ? Result<bool>.Success(true)
                : Result<bool>.Failure("Invalid Password"));
        }
    }
}
