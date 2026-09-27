namespace exam_system.Features.Attempts.CheckRemainingTime.Dtos
{
    public record AttemptScoreDto(double Score, bool Passed , List<AnswerCorrectnessDto> Answers);

}
