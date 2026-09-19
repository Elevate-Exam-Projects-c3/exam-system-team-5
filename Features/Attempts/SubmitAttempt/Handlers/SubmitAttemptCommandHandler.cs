using exam_system.Common.Enums;
using exam_system.Common.Middleware;
using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.SubmitAttempt.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Attempts.SubmitAttempt.Handlers
{
    public class SubmitAttemptCommandHandler(IGenericRepository<QuizAttempt> repository) : IRequestHandler<SubmitAttemptCommand, RequestResponse<bool>>
    {
        public async Task<RequestResponse<bool>> Handle(SubmitAttemptCommand request, CancellationToken cancellationToken)
        {
            // Ensure attempt is loaded with its Quiz navigation property
            var attempt = await repository.GetAll()
                .Where(a => a.Id == request.AttemptId)
                .Include(a => a.Quiz)
                .FirstOrDefaultAsync(a => a.Id == request.AttemptId, cancellationToken);
            var passScore = attempt.Quiz.PassScore;
            if (request.Score >= passScore)
            {
                attempt.Passed = true;
            }

            else
            {
                attempt.Passed = false;
            }

            if (attempt is null)
            {
                throw new NotFoundException("The attempt was not found");
            }
            if (attempt.Status == AttemptStatus.Submitted)
            {
                return RequestResponse<bool>.Fail("Attempt already submitted", 400);
            }
            if (attempt.Status == AttemptStatus.TimedOut)
            {
                attempt.SubmittedAt = attempt.Deadline;
                attempt.Score = request.Score;
                await repository.UpdateAsync(attempt);
                return RequestResponse<bool>.Fail("Attempt is already timed out", 400);
            }
            if (attempt.Deadline < DateTime.UtcNow)
            {
                attempt.Status = AttemptStatus.TimedOut;
                attempt.SubmittedAt = attempt.Deadline;
                attempt.Score = request.Score;
                await repository.UpdateAsync(attempt);
                return RequestResponse<bool>.Fail("Attempt is timed out", 400);
            }



            attempt.Status = AttemptStatus.Submitted;
            attempt.SubmittedAt = DateTime.UtcNow;
            attempt.Score = request.Score;
            await repository.UpdateAsync(attempt);
            return RequestResponse<bool>.Ok(true, "Attempt submitted successfully");
        }
    }
}
