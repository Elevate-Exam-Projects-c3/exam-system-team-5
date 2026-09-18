using System.ComponentModel.DataAnnotations;

namespace exam_system.Infrastructure.Settings
{
    public record EmailSettings
    {
        [Required, EmailAddress]
        public string Email { get; init; } = string.Empty;
        [Required]
        public string Password { get; init; } = string.Empty;
        [Required]
        public string SmtpServer { get; init; } = string.Empty;
        [Range(1, 65535)]
        public int Port { get; init; }
        
    }
}
