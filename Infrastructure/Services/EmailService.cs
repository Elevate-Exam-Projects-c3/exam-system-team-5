using exam_system.Common.Interfaces;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;

namespace exam_system.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
           => _config = config;
        public async Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken)
        {
            // Create a new email message

            var email = new MimeMessage();
            // Set the sender's email address from configuration

            email.From.Add(MailboxAddress.Parse(_config["EmailSettings:Email"]!));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = body };
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["EmailSettings:SmtpServer"]!,
                int.Parse(_config["EmailSettings:Port"]!),
                SecureSocketOptions.StartTls, cancellationToken);

            await smtp.AuthenticateAsync(_config["EmailSettings:Email"]!,
                _config["EmailSettings:Password"]!, cancellationToken);

            await smtp.SendAsync(email, cancellationToken);

            await smtp.DisconnectAsync(true, cancellationToken);
        }
    }
}
