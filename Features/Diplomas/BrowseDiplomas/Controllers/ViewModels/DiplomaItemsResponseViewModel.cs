namespace exam_system.Features.Diplomas.BrowseDiplomas.Controllers.ViewModels
{
    public class DiplomaItemsResponseViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int StudentProgressPercentage { get; set; }
        public int TotalQuizzes { get; set; }

    }
}
