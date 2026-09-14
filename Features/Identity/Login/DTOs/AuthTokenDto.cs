namespace exam_system.Features.Identity.Login.DTOs
{
    public record AuthTokenDto(string AccessToken, string RefreshToken, DateTime RefreshTokenExpiresAt); 
}
