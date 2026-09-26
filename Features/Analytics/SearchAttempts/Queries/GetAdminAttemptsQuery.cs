using exam_system.Common.Enums;
using exam_system.Features.Analytics.SearchAttempts.DTOs;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Analytics.SearchAttempts.Queries
{
    public record GetAdminAttemptsQuery(
        Guid? QuizId = null,
        Guid? StudentId = null,
        AttemptStatus? Status = null,
        bool SortDescending = true,
        int PageIndex = 1,
        int PageSize = 10) : IRequest<PaginatedResult<AdminAttemptDto>>;
}
