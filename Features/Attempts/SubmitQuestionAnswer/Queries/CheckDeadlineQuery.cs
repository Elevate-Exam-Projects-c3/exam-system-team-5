using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Queries
{
    public record CheckDeadlineQuery(DateTime Deadline) : IRequest<RequestResponse<bool>>;


}
