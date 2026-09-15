using exam_system.Common.Enums;
using exam_system.Common.Middleware;
using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminUnpublishQuiz.Commands;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Quizzes.AdminUnpublishQuiz.Handlers
{
    public class UnpublishQuizCommandHandler
        : IRequestHandler<UnpublishQuizCommand, Unit>
    {
        private readonly IGenericRepository<Quiz> _quizRepository;

        public UnpublishQuizCommandHandler(
            IGenericRepository<Quiz> quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<Unit> Handle(
            UnpublishQuizCommand request,
            CancellationToken cancellationToken)
        {
            var quiz = await _quizRepository.GetByIdAsync(request.QuizId);

            if (quiz == null)
                throw new NotFoundException("Quiz not found.");

            quiz.Status = QuizStatus.Draft;
            quiz.PublishedAt = null;
            quiz.UpdatedAt = DateTime.UtcNow;

            _quizRepository.Update(quiz);

            return Unit.Value;
        }
    }
}
