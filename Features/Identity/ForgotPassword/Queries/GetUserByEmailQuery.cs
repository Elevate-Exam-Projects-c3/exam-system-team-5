using exam_system.Common.Results;
using exam_system.Features.Identity.ForgotPassword.Dtos;
using MediatR;

namespace exam_system.Features.Identity.ForgotPassword.Queries
{
    public record GetUserByEmailQuery(string Email) : IRequest<Result<UserResetPasswordDto>>;
}
