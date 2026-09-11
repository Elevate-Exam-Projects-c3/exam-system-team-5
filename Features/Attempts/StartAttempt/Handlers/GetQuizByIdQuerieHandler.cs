using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Attempts.StartAttempt.Queries;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Attempts.StartAttempt.Handlers
{
    public class GetQuizByIdQuerieHandler(IGenericRepository<Quiz> repository) : IRequestHandler<GetQuizByIdQuerie, RequestResponse<Quiz>>
    {
        public async Task<RequestResponse<Quiz>> Handle(GetQuizByIdQuerie request, CancellationToken cancellationToken)
        {
            var result=await repository.GetByIdAsync(request.QuizId);
            return RequestResponse<Quiz>.Ok(result);
        }
    }
}
