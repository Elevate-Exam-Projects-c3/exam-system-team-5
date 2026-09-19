namespace exam_system.Features.Diplomas.GetDiplomaDetail.Dtos
{
    public record DiplomaDetailResponseDto(
      Guid Id,
      string Title,
      string? Description,
      List<QuizSummaryResponseDto> Quizzes);

}
