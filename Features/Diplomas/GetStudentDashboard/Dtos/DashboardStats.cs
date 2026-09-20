namespace exam_system.Features.Diplomas.GetStudentDashboard.Dtos
{
    public record DashboardStats(decimal AverageScore,decimal PassRate,int CompletedAttemptsCount, TimeSpan TotalTimeSpent);
}

