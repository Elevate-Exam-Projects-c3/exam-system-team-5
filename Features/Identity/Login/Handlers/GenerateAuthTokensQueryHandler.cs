using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using exam_system.Features.Identity.Login.DTOs;
using exam_system.Features.Identity.Login.Queries;
using exam_system.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace exam_system.Features.Identity.Login.Handlers
{
    public class GenerateAuthTokensQueryHandler : IRequestHandler<GenerateAuthTokensQuery, AuthTokenDto>
    {
        private readonly JwtSettings _jwtSettings;

        public GenerateAuthTokensQueryHandler(IOptions<JwtSettings> jwtOptions)
        => _jwtSettings = jwtOptions.Value;

        public Task<AuthTokenDto> Handle(GenerateAuthTokensQuery request, CancellationToken cancellationToken)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, request.UserId.ToString()),
                new Claim(ClaimTypes.Email, request.email),
                new Claim(ClaimTypes.Role, request.UserRole.ToString())
            };
            var Key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var cred = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: cred);
            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            string refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            DateTime refreshTokenExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays);
            var reponse = new AuthTokenDto(accessToken, refreshToken, refreshTokenExpiresAt);
            return Task.FromResult(reponse);
        }
    }
}
