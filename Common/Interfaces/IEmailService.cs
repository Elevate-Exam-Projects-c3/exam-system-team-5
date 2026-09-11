namespace exam_system.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to ,string subject ,string body,CancellationToken cancellationToken);
    }
}
