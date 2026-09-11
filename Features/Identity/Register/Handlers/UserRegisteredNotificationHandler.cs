using exam_system.Features.Identity.Register.Events;
using exam_system.Infrastructure.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Text.Encodings.Web;
using System.Threading.Channels;

namespace exam_system.Features.Identity.Register.Handlers
{
    public class UserRegisteredNotificationHandler : INotificationHandler<UserRegisteredNotification>
    {
        private readonly Channel<MimeMessage> _channel;
        private readonly IOptions<EmailSettings> _emailSettings;

        public UserRegisteredNotificationHandler(Channel<MimeMessage> channel,IOptions<EmailSettings> emailSettings)
        {
            _channel = channel;
            _emailSettings = emailSettings;
        }
        public async Task Handle(UserRegisteredNotification notification, CancellationToken cancellationToken)
        {
            string fullName = HtmlEncoder.Default.Encode(notification.FullName);
            string otp = HtmlEncoder.Default.Encode(notification.PlainOtp);
            string emailBody = $@"
                <h2>Welcome to Exam System</h2>
                <p>Hello {fullName},</p>
                <p>Your verification code is: <strong>{otp}</strong></p>
                <p>This code will expire in 10 minutes.</p>";
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailSettings.Value.Email));
            email.To.Add(MailboxAddress.Parse(notification.Email));
            email.Subject = "Verify Your Email";

            var builder = new BodyBuilder { HtmlBody = emailBody };
            email.Body = builder.ToMessageBody();

            await _channel.Writer.WriteAsync(email, cancellationToken);
        }
    }
}
