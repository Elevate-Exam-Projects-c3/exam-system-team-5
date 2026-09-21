namespace exam_system.Features.Diplomas.GetStudentDashboard.Controllers.ViewModels
{
    public record StudentDashboardResponseViewModel(
        List<EnrolledDiplomaSummaryViewModel> EnrolledDiplomas,
        List<RecentAttemptSummaryViewModel> RecentAttempts,
        DashboardStatsViewModel Stats);
}
