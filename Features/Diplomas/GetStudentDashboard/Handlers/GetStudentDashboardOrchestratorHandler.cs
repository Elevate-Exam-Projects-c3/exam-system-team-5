
using exam_system.Common.Enums;
using exam_system.Features.Diplomas.GetStudentDashboard.Dtos;
using exam_system.Features.Diplomas.GetStudentDashboard.Orchestrators;
using exam_system.Features.Diplomas.GetStudentDashboard.Queries;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Diplomas.GetStudentDashboard.Handlers
{
    public class GetStudentDashboardOrchestratorHandler : IRequestHandler<GetStudentDashboardOrchestrator, RequestResponse<StudentDashboardResponseDto>>
    {
        private readonly IMediator _mediator;
        public GetStudentDashboardOrchestratorHandler(IMediator mediator) => _mediator = mediator;
        public async Task<RequestResponse<StudentDashboardResponseDto>> Handle(GetStudentDashboardOrchestrator request, CancellationToken cancellationToken)
        {
            var diplomasResult = await _mediator.Send(new GetEnrolledDiplomasQuery(request.StudentId), cancellationToken);
            var attemptsResult = await _mediator.Send(new GetStudentAttemptsQuery(request.StudentId), cancellationToken);

            var recentAttempts = attemptsResult.Data
                .OrderByDescending(a => a.SubmittedAt ?? a.StartTime)
                .Take(5)
                .Select(a => new RecentAttemptSummaryDto(
                    a.Id, 
                    a.QuizId, 
                    a.QuizTitle, 
                    a.Status.ToString(), 
                    a.Score, 
                    a.Passed, 
                    a.StartTime, 
                    a.SubmittedAt)).ToList();

            var completedQuizzes = attemptsResult.Data
                .Where(a => a.Status is AttemptStatus.Submitted or AttemptStatus.TimedOut)
                .ToList();

            var stats = new DashboardStatsDto(
                AverageScore: completedQuizzes.Count > 0 ? completedQuizzes.Average(a => a.Score ?? 0) : 0,
                PassRate: completedQuizzes.Count > 0 ? (decimal)completedQuizzes.Count(a => a.Passed == true) / completedQuizzes.Count * 100 : 0,
                CompletedAttemptsCount: completedQuizzes.Count,
                TotalTimeSpent: completedQuizzes.Aggregate(TimeSpan.Zero,
                    (sum, a) => sum + (a.SubmittedAt.HasValue ? a.SubmittedAt.Value - a.StartTime : TimeSpan.Zero)));

            var response = new StudentDashboardResponseDto(diplomasResult.Data, recentAttempts, stats);
            return RequestResponse<StudentDashboardResponseDto>.Ok((StudentDashboardResponseDto)response); 
        }
    }
}
