using exam_system.Features.Quizzes.AdminQuizPublishCheck.Dtos;
using MediatR;

namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Queries
{
    public record GetQuestionOptionsQuery(
        IReadOnlyCollection<Guid> QuestionIds
    ) : IRequest<List<GetQuestionOptionsResponse>>;
}
