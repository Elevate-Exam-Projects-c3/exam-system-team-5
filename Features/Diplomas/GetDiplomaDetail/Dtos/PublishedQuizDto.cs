namespace exam_system.Features.Diplomas.GetDiplomaDetail.Dtos
{
    public record PublishedQuizDto(Guid Id, string Title, int DurationMinutes, int PassScore, int? MaxAttempts);

}
