namespace exam_system.Features.Diplomas.GetStudentDashboard.Dtos
{
    public record DashboardStatsDto(decimal AverageScore,decimal PassRate,int CompletedAttemptsCount, TimeSpan TotalTimeSpent);
}

