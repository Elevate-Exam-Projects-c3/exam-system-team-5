
using exam_system.Common.Middleware;
using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminManageQuestions.Commands;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Quizzes.AdminManageQuestions.Handlers
{
    public class UpdateQuestionCommandHandler
         : IRequestHandler<UpdateQuestionCommand, Unit>
    {
        private readonly IGenericRepository<Question> _questionRepository;

        public UpdateQuestionCommandHandler(
            IGenericRepository<Question> questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<Unit> Handle(
            UpdateQuestionCommand request,
            CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetByIdAsync(
                request.QuestionId);

            if (question == null)
            {
                throw new NotFoundException(
                    "Question not found.");
            }


            question.Text = request.Text;
            question.Explanation = request.Explanation;
            question.OrderIndex = request.OrderIndex;
            question.UpdatedAt = DateTime.UtcNow;

            _questionRepository.Update(question);

            return Unit.Value;
        }
    }
}
