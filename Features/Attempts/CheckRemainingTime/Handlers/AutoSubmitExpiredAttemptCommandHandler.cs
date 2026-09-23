using exam_system.Common.Enums;
using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.CheckRemainingTime.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Attempts.CheckRemainingTime.Handlers
{
    public class AutoSubmitExpiredAttemptCommandHandler : IRequestHandler<AutoSubmitExpiredAttemptCommand, RequestResponse<bool>>
    {
        private readonly IGenericRepository<QuizAttempt> _attemptRepository;
        public AutoSubmitExpiredAttemptCommandHandler(IGenericRepository<QuizAttempt> attemptRepository)
            => _attemptRepository = attemptRepository;
        public async Task<RequestResponse<bool>> Handle(AutoSubmitExpiredAttemptCommand command, CancellationToken cancellationToken)
        {
            var attemptInfo = await _attemptRepository.Get(a => a.Id == command.AttemptId)
                    .Select(a => new
                    {
                        a.Status,
                        a.Deadline,
                        PassScore = a.Quiz.PassScore,
                        TotalAnswers = a.Answers.Count,
                        CorrectAnswers = a.Answers.Count(ans => ans.SelectedOption != null && ans.SelectedOption.IsCorrect)
                    })
                    .FirstOrDefaultAsync(cancellationToken);

            if (attemptInfo is null)
                return RequestResponse<bool>.Fail("Attempt not found.", 404);

            if (attemptInfo.Status != AttemptStatus.InProgress || DateTime.UtcNow < attemptInfo.Deadline)
                return RequestResponse<bool>.Ok(false);

            var score = attemptInfo.TotalAnswers > 0
                ? (double)(attemptInfo.CorrectAnswers / attemptInfo.TotalAnswers * 100): 0;

            await _attemptRepository.UpdateAsync(
                a => a.Id == command.AttemptId,
                setters => setters
                    .SetProperty(a => a.Status, AttemptStatus.TimedOut)
                    .SetProperty(a => a.Score, score)
                    .SetProperty(a => a.Passed, score >= attemptInfo.PassScore)
                    .SetProperty(a => a.SubmittedAt, DateTime.UtcNow),
                cancellationToken);

            return RequestResponse<bool>.Ok(true);
        }
    }
}
