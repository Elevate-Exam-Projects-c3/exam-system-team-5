using exam_system.Features.Diplomas.GetDiplomaDetail.Dtos;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Diplomas.GetDiplomaDetail.Queries
{
    public record GetStudentAttemptsForQuizzesQuery(Guid StudentId, List<Guid> QuizIds) : IRequest<RequestResponse<List<StudentQuizAttemptDto>>>;

}
