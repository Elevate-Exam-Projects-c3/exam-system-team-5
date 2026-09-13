using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminManageQuestions.Commands;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Quizzes.AdminManageQuestions.Handlers
{
    public class AddQuestionCommandHandler
        : IRequestHandler<AddQuestionCommand, Unit>
    {
        private readonly IGenericRepository<Question> _questionRepository;

        public AddQuestionCommandHandler(
            IGenericRepository<Question> questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<Unit> Handle(
            AddQuestionCommand request,
            CancellationToken cancellationToken)
        {
            var question = new Question
            {
                Id = request.QuestionId,
                QuizId = request.QuizId,
                Text = request.Text,
                Explanation = request.Explanation,
                OrderIndex = request.OrderIndex
            };

            await _questionRepository.AddAsync(question);

            return Unit.Value;
        }
    }
}
