using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Orchestrators
{
    public record AnswerQuestionOrchestrator(
        Guid AttemptId,
        Guid QuestionId,
        Guid? SelectedOptionId
    ) : IRequest<RequestResponse<uint>>;
}
