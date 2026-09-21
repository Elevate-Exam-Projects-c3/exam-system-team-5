namespace exam_system.Features.Diplomas.GetStudentDashboard.Controllers.ViewModels
{
    public record DashboardStatsViewModel(decimal AverageScore,decimal PassRate,int CompletedAttemptsCount, TimeSpan TotalTimeSpent);
}

