using exam_system.Common.Enums;
using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Attempts.StartAttempt.Commands;
using exam_system.Features.Attempts.StartAttempt.Orchestrators;
using exam_system.Features.Attempts.StartAttempt.Queries;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Attempts.StartAttempt.Handlers
{
    public class StartAttemptOrchestratorHandler(IMediator mediator): IRequestHandler<StartAttemptOrchestrator, RequestResponse<bool>>
    {
        public async Task<RequestResponse<bool>> Handle(StartAttemptOrchestrator request, CancellationToken cancellationToken)
        {

            var quiz = await mediator.Send(new GetQuizByIdQuerie(request.QuizId));
            if (quiz.Data is null)
            {
                return RequestResponse<bool>.Fail(
                    "Quiz not found.",
                    404);
            }

            if (quiz.Data.Status != QuizStatus.Published)
            {
                return RequestResponse<bool>.Fail(
                    "Quiz is not published.",
                    409);
            }

            var result =await mediator.Send(new CreateAttemptCommand(quiz.Data, request.StudentId), cancellationToken);
            return result;
        }
    }
}
