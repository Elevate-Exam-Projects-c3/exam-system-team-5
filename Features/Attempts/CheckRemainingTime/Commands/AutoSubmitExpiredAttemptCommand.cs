using exam_system.Features.Shared;
using MediatR;
using System.Windows.Input;

namespace exam_system.Features.Attempts.CheckRemainingTime.Commands
{
    public record AutoSubmitExpiredAttemptCommand(Guid AttemptId) : IRequest<RequestResponse<bool>>;

}
