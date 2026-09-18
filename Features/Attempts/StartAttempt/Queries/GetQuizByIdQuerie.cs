using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Attempts.StartAttempt.Queries
{
    public record GetQuizByIdQuerie(Guid QuizId) : IRequest<RequestResponse<Quiz>>;
}
