using exam_system.Common.Enums;
using exam_system.Features.Analytics.SearchAttempts.Queries;

namespace exam_system.Features.Analytics.SearchAttempts.ViewModels
{
    public record GetAdminAttemptsViewModel(
        Guid? QuizId = null,
        Guid? StudentId = null,
        AttemptStatus? Status = null,
        bool SortDescending = true,
        int PageIndex = 1,
        int PageSize = 10
        )
    {
        public GetAdminAttemptsQuery ToDto() =>
        new(QuizId, StudentId, Status, SortDescending, PageIndex, PageSize);
    }
    
}
