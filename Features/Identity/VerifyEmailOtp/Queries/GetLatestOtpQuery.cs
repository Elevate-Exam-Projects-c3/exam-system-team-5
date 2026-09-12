using exam_system.Features.Identity.VerifyEmailOtp.DTOs;
using MediatR;

namespace exam_system.Features.Identity.VerifyEmailOtp.Queries
{
    public record GetLatestOtpQuery(string Email) : IRequest<OtpVerificationDto?>;  
}
