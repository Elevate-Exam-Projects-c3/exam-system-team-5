using exam_system.Common.Enums;
using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.CheckRemainingTime.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace exam_system.Features.Attempts.CheckRemainingTime.Handlers
{
    public class AutoSubmitExpiredAttemptCommandHandler : IRequestHandler<AutoSubmitExpiredAttemptCommand, RequestResponse<bool>>
    {
        private readonly IGenericRepository<QuizAttempt> _attemptRepository;
        public AutoSubmitExpiredAttemptCommandHandler(IGenericRepository<QuizAttempt> attemptRepository)
            => _attemptRepository = attemptRepository;
        public async Task<RequestResponse<bool>> Handle(AutoSubmitExpiredAttemptCommand command, CancellationToken cancellationToken)
        {
            var attempt = await _attemptRepository.Get(a => a.Id == command.AttemptId)
                .FirstOrDefaultAsync(cancellationToken);

            if (attempt is null)
                return RequestResponse<bool>.Fail("Attempt not found.", 404);

            // مش in_progress أصلًا (خلاص Submitted/TimedOut) → مفيش حاجة تتعمل، مش خطأ
            if (attempt.Status != AttemptStatus.InProgress)
                return RequestResponse<bool>.Ok(false);

            // لسه في وقته → مفيش auto-submit
            if (DateTime.UtcNow < attempt.Deadline)
                return RequestResponse<bool>.Ok(false);

            // ⚠️ هنا بالظبط لازم أعرف: عندك منطق تصحيح جاهز من EXAM-22 (Submit & Score)؟
            // المفروض السطرين دول ينادوا **نفس** الكود اللي EXAM-22 بيستخدمه، مش نسخة تانية منفصلة
            foreach (var answer in attempt.Answers)
                answer.IsCorrect = answer.SelectedOption?.IsCorrect ?? false;

            var correctCount = attempt.Answers.Count(a => a.IsCorrect == true);
            //attempt.Score = attempt.Answers.Count > 0 ? (decimal)correctCount / attempt.Answers.Count * 100 : 0;
            attempt.Passed = attempt.Score >= attempt.Quiz.PassScore;
            attempt.Status = AttemptStatus.TimedOut;
            attempt.SubmittedAt = DateTime.UtcNow;

            return RequestResponse<bool>.Ok(true);
        }
    }
}
