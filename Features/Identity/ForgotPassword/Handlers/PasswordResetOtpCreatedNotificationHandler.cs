using exam_system.Features.Identity.ForgotPassword.Events;
using exam_system.Infrastructure.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Text.Encodings.Web;
using System.Threading.Channels;

namespace exam_system.Features.Identity.ForgotPassword.Handlers
{
    public class PasswordResetOtpCreatedNotificationHandler : INotificationHandler<PasswordResetOtpCreatedNotification>
    {
        private readonly Channel<MimeMessage> _channel;
        private readonly IOptions<EmailSettings> _emailSetting;

        public PasswordResetOtpCreatedNotificationHandler(Channel<MimeMessage> channel,IOptions<EmailSettings>emailSetting)
        {
            _channel = channel;
            _emailSetting = emailSetting;
        }
        public async Task Handle(PasswordResetOtpCreatedNotification notification, CancellationToken cancellationToken)
        {

            string safeName = HtmlEncoder.Default.Encode(notification.FullName); 
            string safeOtp = HtmlEncoder.Default.Encode(notification.PlainOtp);
            string body = $@"
            <h2>Password Reset Request</h2>
            <p>Hello {safeName},</p>
            <p>You requested to reset your password. Use the following verification code:</p>
            <h3 style='letter-spacing: 5px;'>{safeOtp}</h3>
            <p>This code expires in <strong>10 minutes</strong>.</p>
            <p>If you did not make this request, please ignore this email or change your password immediately.</p>";
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_emailSetting.Value.Email));
            message.To.Add(MailboxAddress.Parse(notification.Email));
            message.Subject = "Reset Your Password - Verification Code";
            var builder = new BodyBuilder { HtmlBody = body };
            message.Body = builder.ToMessageBody();
            await _channel.Writer.WriteAsync(message, cancellationToken);
        }
    }
}
