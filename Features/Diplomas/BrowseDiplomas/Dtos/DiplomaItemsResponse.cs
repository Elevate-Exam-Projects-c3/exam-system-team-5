namespace exam_system.Features.Diplomas.BrowseDiplomas.Dtos
{
    public record DiplomaItemsResponse(
    Guid Id,
    string Title,
    string? Description,
    int StudentProgressPercentage,
    int TotalQuizzes);
}
