namespace exam_system.Features.Diplomas.GetStudentDashboard.Dtos
{
    public record DashboardStatsDto(double AverageScore,decimal PassRate,int CompletedAttemptsCount, TimeSpan TotalTimeSpent);
}

