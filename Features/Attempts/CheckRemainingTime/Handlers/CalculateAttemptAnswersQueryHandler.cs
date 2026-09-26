using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.CheckRemainingTime.Dtos;
using exam_system.Features.Attempts.CheckRemainingTime.Queries;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Attempts.CheckRemainingTime.Handlers
{
    public class CalculateAttemptAnswersQueryHandler : IRequestHandler<CalculateAttemptAnswersQuery, RequestResponse<AttemptScoreDto>>
    {
        private readonly IGenericRepository<QuizAttempt> _attemptRepository;
        public CalculateAttemptAnswersQueryHandler(IGenericRepository<QuizAttempt> attemptRepository)
            => _attemptRepository = attemptRepository;
        public async Task<RequestResponse<AttemptScoreDto>> Handle(CalculateAttemptAnswersQuery request, CancellationToken cancellationToken)
        {
            var attempt = await _attemptRepository.Get(a => a.Id == request.AttemptId)
            .Select(a => new
            {
                PassScore = a.Quiz.PassScore,
                Answers = a.Answers.Select(ans => new
                {
                    ans.Id,
                    IsCorrect = ans.SelectedOption != null && ans.SelectedOption.IsCorrect
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

            if (attempt is null)
                return RequestResponse<AttemptScoreDto>.Fail("Attempt not found.", 404);

            var score = attempt.Answers.Count > 0
                ? (double)attempt.Answers.Count(a => a.IsCorrect) / attempt.Answers.Count * 100: 0;

            var answerResults = attempt.Answers.Select(a => new AnswerCorrectnessDto(a.Id, a.IsCorrect)).ToList();

            return RequestResponse<AttemptScoreDto>.Ok(new AttemptScoreDto(score, score >= attempt.PassScore, answerResults));
        }
    }
}
