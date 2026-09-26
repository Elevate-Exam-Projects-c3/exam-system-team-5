using exam_system.Features.Attempts.SubmitQuestionAnswer.Dtos;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Commands
{
    public record AddQuestionAnswerCommand(Guid QuestionId, OptionDto Option, Guid AttemptId) : IRequest<RequestResponse<uint>>;

}
