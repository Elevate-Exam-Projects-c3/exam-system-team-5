namespace exam_system.Features.Diplomas.GetStudentDashboard.Dtos
{
    public record StudentDashboardResponse(
        List<EnrolledDiplomaSummary> EnrolledDiplomas,
        List<RecentAttemptSummary> RecentAttempts,
        DashboardStats Stats);
}
