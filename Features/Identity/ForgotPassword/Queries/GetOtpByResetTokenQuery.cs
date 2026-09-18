using exam_system.Common.Results;
using exam_system.Features.Identity.ForgotPassword.Dtos;
using MediatR;

namespace exam_system.Features.Identity.ForgotPassword.Queries
{
    public record GetOtpByResetTokenQuery(string Email, string ResetToken) : IRequest<Result<ResetPasswordOtpDto>>;

}
