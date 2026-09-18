using MediatR;

namespace exam_system.Features.Identity.Register.Events
{
    public record UserRegisteredNotification(string Email, string FullName, string PlainOtp) : INotification;
    
}