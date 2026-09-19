namespace exam_system.Features.Diplomas.GetDiplomaDetail.Dtos
{
    public record QuizSummaryResponseDto(
        Guid Id,
        string Title,
        int DurationMinutes,
        int PassScore,
        bool CanAttempt,
        bool IsResumable);
}
