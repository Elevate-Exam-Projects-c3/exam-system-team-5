using exam_system.Common.Enums;

namespace exam_system.Features.Identity.Login.DTOs
{
    public record ActiveUserDto(
        Guid Id, 
        string FullName,
        string Email,
        UserRole UserRole, 
        string PasswordHash,
        int FailedLoginAttempts);
}
