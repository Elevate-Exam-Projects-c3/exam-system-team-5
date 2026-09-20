// Features/Students/StudentDashboard/Handlers/GetStudentDashboardQueryHandler.cs
using exam_system.Common.Enums;
using exam_system.Domain.Entities.Attempts;
using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.GetStudentDashboard.Dtos;
using exam_system.Features.Diplomas.GetStudentDashboard.Queries;
using exam_system.Features.Shared;
using exam_system.Features.Students.StudentDashboard.Queries;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Students.StudentDashboard.Handlers
{
    public class GetStudentDashboardQueryHandler
        : IRequestHandler<GetStudentDashboardQuery, RequestResponse<StudentDashboardResponse>>
    {
        private readonly IGenericRepository<StudentEnrollment> _enrollmentRepository;
        private readonly IGenericRepository<QuizAttempt> _attemptRepository;

        public GetStudentDashboardQueryHandler(
            IGenericRepository<StudentEnrollment> enrollmentRepository,
            IGenericRepository<QuizAttempt> attemptRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _attemptRepository = attemptRepository;
        }

        public async Task<RequestResponse<StudentDashboardResponse>> Handle(
            GetStudentDashboardQuery request, CancellationToken cancellationToken)
        {
            // رحلة 1: الدبلومات اللي الطالب ملتحق بيها
            var enrolledDiplomas = await _enrollmentRepository
                .Get(e => e.StudentId == request.StudentId)
                .Select(e => new EnrolledDiplomaSummary(e.DiplomaId, e.Diploma.Title, e.EnrolledAt))
                .ToListAsync(cancellationToken);

            var attempts = await _attemptRepository
                .Get(a => a.StudentId == request.StudentId)
                .Select(a => new
                {
                    a.Id,
                    a.QuizId,
                    QuizTitle = a.Quiz.Title,
                    a.Status,
                    a.Score,
                    a.Passed,
                    a.StartTime,
                    a.SubmittedAt
                })
                .ToListAsync(cancellationToken);

            var recentAttempts = attempts
                .OrderByDescending(a => a.SubmittedAt ?? a.StartTime)
                .Take(5)
                .Select(a => new RecentAttemptSummary(
                    a.Id, 
                    a.QuizId, 
                    a.QuizTitle, 
                    a.Status.ToString(),
                    a.Score, 
                    a.Passed, 
                    a.StartTime, 
                    a.SubmittedAt))
                .ToList();

            var completed = attempts
                .Where(a => a.Status is AttemptStatus.Submitted or AttemptStatus.TimedOut)
                .ToList();

            var stats = new DashboardStats(
                AverageScore: completed.Count > 0 ? completed.Average(a => a.Score ?? 0) : 0,
                PassRate: completed.Count > 0 ? (decimal)completed.Count(a => a.Passed == true) / completed.Count * 100 : 0,
                CompletedAttemptsCount: completed.Count,
                TotalTimeSpent: completed.Aggregate(TimeSpan.Zero,
                    (sum, a) => sum + (a.SubmittedAt.HasValue ? a.SubmittedAt.Value - a.StartTime : TimeSpan.Zero)));

            var response = new StudentDashboardResponse(enrolledDiplomas, recentAttempts, stats);
            return RequestResponse<StudentDashboardResponse>.Ok(response);
        }
    }
}