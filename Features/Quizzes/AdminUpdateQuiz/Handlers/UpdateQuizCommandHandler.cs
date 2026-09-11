using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminUpdateQuiz.Commands;
using exam_system.Features.Shared;
using exam_system.Features.Shared.DTOs;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Quizzes.AdminUpdateQuiz.Handlers
{
    public class UpdateQuizCommandHandler : IRequestHandler<UpdateQuizCommand, RequestResponse<bool>>
    {
        private readonly IGenericRepository<Quiz> quizRepository;

        public UpdateQuizCommandHandler(IGenericRepository<Quiz> quizRepository)
        {
            this.quizRepository = quizRepository;
        }

        public async Task<RequestResponse<bool>> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
        {
            // load existing entity
            var quiz = await quizRepository.GetByIdAsync(request.QuizId);
            if (quiz == null)
            {
                return RequestResponse<bool>.Fail("Quiz not found", 404);
            }

            quiz.Title = request.Title;
            quiz.Instructions = request.Instructions;
            quiz.DurationMinutes = request.DurationMinutes;
            quiz.StartDate = request.StartDate;
            quiz.EndDate = request.EndDate;
            quiz.PassScore = request.PassScore;
            quiz.MaxAttempts = request.MaxAttempts;

            await quizRepository.UpdateAsync(quiz);

            return RequestResponse<bool>.Created(true, "Quiz updated successfully");
        }
    }
}
