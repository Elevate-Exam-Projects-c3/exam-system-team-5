namespace exam_system.Features.Diplomas.GetStudentDashboard.Dtos
{
    public record StudentDashboardResponseDto(
        List<EnrolledDiplomaSummaryDto> EnrolledDiplomas,
        List<RecentAttemptSummaryDto> RecentAttempts,
        DashboardStatsDto Stats);
}
