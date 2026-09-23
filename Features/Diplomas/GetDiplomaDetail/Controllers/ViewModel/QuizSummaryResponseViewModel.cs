namespace exam_system.Features.Diplomas.GetDiplomaDetail.Controllers.ViewModel
{
    public record QuizSummaryResponseViewModel(
        Guid Id,
        string Title,
        int DurationMinutes,
        int PassScore,
        bool CanAttempt,
        bool IsResumable);
}
