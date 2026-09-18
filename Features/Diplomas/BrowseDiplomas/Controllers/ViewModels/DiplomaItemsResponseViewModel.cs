namespace exam_system.Features.Diplomas.BrowseDiplomas.Controllers.ViewModels
{
    public record DiplomaItemsResponseViewModel(
    string Id,
    string Title,
    string Description,
    int StudentCompletedQuizzesCount,
    int TotalQuizzes);
}
