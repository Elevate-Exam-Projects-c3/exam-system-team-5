using exam_system.Features.Attempts.SubmitQuestionAnswer.Dtos;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Queries
{
    public record GetQuestionByIdQuery(Guid QuestionId) : IRequest<RequestResponse<QuestionDto>>;
}
