using exam_system.Features.Attempts.CheckRemainingTime.Dtos;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Attempts.CheckRemainingTime.Commands
{
    public record UpdateAnswersCorrectnessCommand(List<AnswerCorrectnessDto> Answers) : IRequest<RequestResponse<Unit>>;
}
