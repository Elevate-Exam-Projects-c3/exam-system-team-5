namespace exam_system.Features.Diplomas.BrowseDiplomas.Dtos
{
    public record DiplomaItemsResponseDto(
    Guid Id,
    string Title,
    string? Description,
    int StudentCompletedQuizzesCount,
    int TotalQuizzes);
}
