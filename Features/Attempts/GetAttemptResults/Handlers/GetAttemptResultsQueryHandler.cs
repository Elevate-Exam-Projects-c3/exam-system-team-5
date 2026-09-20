using exam_system.Common.Enums;
using exam_system.Common.Middleware;
using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.GetAttemptResults.Models;
using exam_system.Features.Attempts.GetAttemptResults.Queries;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Attempts.GetAttemptResults.Handlers
{
    public class GetAttemptResultsQueryHandler(IGenericRepository<QuizAttempt> repository) : IRequestHandler<GetAttemptResultsQuery, RequestResponse<AttemptResultResponse>>
    {
        public async Task<RequestResponse<AttemptResultResponse>> Handle(GetAttemptResultsQuery request, CancellationToken cancellationToken)
        {
            var attempt = await repository.GetAll()
                .Where(a => a.Id == request.AttemptId && a.StudentId == request.StudentId)
                .Select(a => new
                {
                    a.Id,
                    a.Status,
                    a.Score,
                    a.Passed,
                    Answers = a.Answers
                        .OrderBy(x => x.Question.OrderIndex)
                        .Select(x => new
                        {
                            x.QuestionId,
                            QuestionText = x.Question.Text,
                            x.SelectedOptionId,
                            SelectedAnswer = x.SelectedOption != null
                                ? x.SelectedOption.OptionText
                                : null,
                            x.IsCorrect,
                            CorrectAnswer = x.Question.Options
                                .Where(o => o.IsCorrect)
                                .Select(o => o.OptionText)
                                .FirstOrDefault(),
                            x.Question.Explanation
                        })
                        .ToList()
                }).FirstOrDefaultAsync(cancellationToken);

            if (attempt is null)
            {
                throw new NotFoundException("The attempt was not found");
            }

            if (attempt.Status != AttemptStatus.Submitted &&
                attempt.Status != AttemptStatus.TimedOut)
            {
                throw new Exception("Results are not available while the attempt is in progress");
            }

            var response = new AttemptResultResponse
            {
                AttemptId = attempt.Id,
                Status = attempt.Status,
                Score = attempt.Score,
                Passed = attempt.Passed,

                Questions = attempt.Answers
                    .Select(answer => new QuestionResultResponse
                    {
                        QuestionId = answer.QuestionId,
                        QuestionText = answer.QuestionText,
                        SelectedAnswer = answer.SelectedAnswer,
                        IsCorrect = answer.IsCorrect ?? false,
                        CorrectAnswer = answer.CorrectAnswer,
                        Explanation = answer.Explanation
                    })
                    .ToList()
            };

            return RequestResponse<AttemptResultResponse>.Ok(response,"Attempt results retrieved successfully");


        }
    }
}
