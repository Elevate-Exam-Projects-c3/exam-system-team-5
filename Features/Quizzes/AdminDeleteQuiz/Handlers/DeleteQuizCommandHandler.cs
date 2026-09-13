using exam_system.Common.Enums;
using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminDeleteQuiz.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Quizzes.AdminDeleteQuiz.Handlers
{
    public class DeleteQuizCommandHandler(IGenericRepository<Quiz> repository): IRequestHandler<DeleteQuizCommand, RequestResponse<bool>>
    {
        public async Task<RequestResponse<bool>> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
        {
            var Quiz=await repository.Get(q => q.Id == request.QuizId)
                .Include(q=>q.Attempts)
                .FirstOrDefaultAsync(cancellationToken);

            if (Quiz is null ||Quiz.IsDeleted==true)
            {
                return RequestResponse<bool>.Fail("Quiz not found", 404);
            }
            if(Quiz.Status != QuizStatus.Published)
            {
                return RequestResponse<bool>.Fail("Cannot delete a published quiz", 400);
            }
            if(Quiz.Attempts.Any(a=>a.Status == AttemptStatus.InProgress))
            {
                return RequestResponse<bool>.Fail("Quiz cannot be deleted while it has in-progress attempts.", 400);
            }

            Quiz.IsDeleted = true;
            Quiz.DeletedAt = DateTime.UtcNow;
            await repository.UpdateAsync(Quiz);


            return RequestResponse<bool>.Ok(true, "Quiz deleted successfully");
        }
    }
}
