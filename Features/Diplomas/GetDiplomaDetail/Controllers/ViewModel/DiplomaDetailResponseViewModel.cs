using exam_system.Features.Diplomas.GetDiplomaDetail.Dtos;

namespace exam_system.Features.Diplomas.GetDiplomaDetail.Controllers.ViewModel
{
    public record DiplomaDetailResponseViewModel(
      Guid Id,
      string Title,
      string? Description,
      List<QuizSummaryResponseViewModel> Quizzes);
}
