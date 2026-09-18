using exam_system.Infrastructure.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Channels;

namespace exam_system.Infrastructure.Services
{
    public sealed class EmailBackgroundService : BackgroundService
    {
        private const int MaxRetries = 3;
        private const int RetryDelayMilliseconds = 2000;

        private readonly Channel<MimeMessage> _channel;
        private readonly EmailSettings _settings;
        private readonly ILogger<EmailBackgroundService> _logger;

        public EmailBackgroundService(
            Channel<MimeMessage> channel,
            IOptions<EmailSettings> options,
            ILogger<EmailBackgroundService> logger)
        {
            _channel = channel;
            _settings = options.Value;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Email Background Service is starting.");

            using var smtp = new SmtpClient();

            try
            {
                await foreach (
                    var email in _channel.Reader.ReadAllAsync(stoppingToken))
                {
                    var sent = await SendEmailWithRetryAsync(
                        smtp,
                        email,
                        stoppingToken);

                    if (!sent && stoppingToken.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }
            catch (OperationCanceledException)
                when (stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation(
                    "Email Background Service is stopping.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Unexpected error occurred in Email Background Service.");
            }
            finally
            {
                await DisconnectSafelyAsync(smtp);
            }

            _logger.LogInformation(
                "Email Background Service has stopped.");
        }

        private async Task<bool> SendEmailWithRetryAsync(
            SmtpClient smtp,
            MimeMessage email,
            CancellationToken cancellationToken)
        {
            for (int attempt = 1; attempt <= MaxRetries; attempt++)
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (!smtp.IsConnected)
                    {
                        await smtp.ConnectAsync(
                            _settings.SmtpServer,
                            _settings.Port,
                            SecureSocketOptions.StartTls,
                            cancellationToken);

                        await smtp.AuthenticateAsync(
                            _settings.Email,
                            _settings.Password,
                            cancellationToken);
                    }

                    await smtp.SendAsync(
                        email,
                        cancellationToken);

                    _logger.LogInformation(
                        "Email sent successfully to {Email}.",
                        email.To);

                    return true;
                }
                catch (OperationCanceledException)
                    when (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation(
                        "Email sending was cancelled because the service is stopping.");

                    return false;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(
                        ex,
                        "Failed to send email to {Email}. Attempt {Attempt} of {MaxRetries}.",
                        email.To,
                        attempt,
                        MaxRetries);

                    await DisconnectSafelyAsync(smtp);

                    if (attempt == MaxRetries)
                    {
                        _logger.LogError(
                            "Email permanently failed after {MaxRetries} attempts for {Email}.",
                            MaxRetries,
                            email.To);

                        return false;
                    }

                    try
                    {
                        await Task.Delay(
                            RetryDelayMilliseconds,
                            cancellationToken);
                    }
                    catch (OperationCanceledException)
                        when (cancellationToken.IsCancellationRequested)
                    {
                        _logger.LogInformation(
                            "Email retry was cancelled because the service is stopping.");

                        return false;
                    }
                }
            }

            return false;
        }

        private async Task DisconnectSafelyAsync(SmtpClient smtp)
        {
            if (!smtp.IsConnected)
            {
                return;
            }

            try
            {
                await smtp.DisconnectAsync(
                    quit: true,
                    cancellationToken: CancellationToken.None);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(
                    ex,
                    "Failed to disconnect from SMTP server.");
            }
        }
    }
}