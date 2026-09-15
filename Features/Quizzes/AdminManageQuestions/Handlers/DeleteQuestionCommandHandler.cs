using exam_system.Common.Enums;
using exam_system.Common.Middleware;
using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminManageQuestions.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Quizzes.AdminManageQuestions.Handlers
{
    public class DeleteQuestionCommandHandler
        : IRequestHandler<DeleteQuestionCommand,RequestResponse<Unit>>
    {
        private readonly IGenericRepository<Question> _questionRepository;

        public DeleteQuestionCommandHandler(
            IGenericRepository<Question> questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<RequestResponse<Unit>> Handle(
            DeleteQuestionCommand request,
            CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetByIdAsync(
                request.QuestionId,
                q => q.Quiz);

            if (question == null)
            {
                throw new NotFoundException(
                    "Question not found.");
            }

            if (question.Quiz.Status == QuizStatus.Published)
            {
                throw new ConflictException(
                    "Cannot delete a question from a published quiz.");
            }

            _questionRepository.Delete(question);

            return RequestResponse<Unit>.Ok(Unit.Value, "Question deleted successfully.")   ;
        }
    }
}
