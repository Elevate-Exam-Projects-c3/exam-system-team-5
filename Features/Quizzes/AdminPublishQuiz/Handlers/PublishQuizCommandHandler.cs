using exam_system.Common.Enums;
using exam_system.Common.Middleware;
using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminPublishQuiz.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Quizzes.AdminPublishQuiz.Handlers
{
    public class PublishQuizCommandHandler
        : IRequestHandler<PublishQuizCommand, RequestResponse<Unit>>
    {
        private readonly IGenericRepository<Quiz> _quizRepository;

        public PublishQuizCommandHandler(
            IGenericRepository<Quiz> quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<RequestResponse<Unit>> Handle(
            PublishQuizCommand request,
            CancellationToken cancellationToken)
        {
            var quiz = await _quizRepository.GetByIdAsync(request.QuizId);

            if (quiz == null)
                throw new NotFoundException("Quiz not found.");

            quiz.Status = QuizStatus.Published;
            quiz.PublishedAt = DateTime.UtcNow;
            quiz.UpdatedAt = DateTime.UtcNow;

            _quizRepository.Update(quiz);

            return RequestResponse<Unit>.Ok(Unit.Value, "Quiz published successfully");
        }
    }
}
