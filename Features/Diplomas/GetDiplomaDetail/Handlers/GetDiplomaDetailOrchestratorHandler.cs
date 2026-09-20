using exam_system.Common.Enums;
using exam_system.Features.Diplomas.GetDiplomaDetail.Dtos;
using exam_system.Features.Diplomas.GetDiplomaDetail.Orchestrators;
using exam_system.Features.Diplomas.GetDiplomaDetail.Queries;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Diplomas.GetDiplomaDetail.Handlers
{
    public class GetDiplomaDetailOrchestratorHandler : IRequestHandler<GetDiplomaDetailOrchestrator, RequestResponse<DiplomaDetailResponseDto>>
    {
        private readonly IMediator _mediator;
        public GetDiplomaDetailOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        // this query is have a 3 requests to database 

        //first : i get the diploma details by diplomaId in memory
        //second : i get the publihed quizes on this diploma
        //third : i count the quizes attemptes for all quizes one time and its status isResumable or not 
        public async Task<RequestResponse<DiplomaDetailResponseDto>> Handle(GetDiplomaDetailOrchestrator request, CancellationToken cancellationToken)
        {
            var diplomaResult = await _mediator.Send(new GetDiplomaBasicInfoQuery(request.DiplomaId), cancellationToken);
            if (!diplomaResult.Success)
                return RequestResponse<DiplomaDetailResponseDto>.Fail(diplomaResult.Message, diplomaResult.StatusCode);

            var quizzesResult = await _mediator.Send(new GetPublishedQuizzesByDiplomaQuery(request.DiplomaId), cancellationToken);

            var quizIds = quizzesResult.Data.Select(q => q.Id).ToList();
            var attemptsResult = await _mediator.Send(new GetStudentAttemptsForQuizzesQuery(request.StudentId, quizIds), cancellationToken);

            var quizSummaries = quizzesResult.Data.Select(q =>
            {
                var quizAttempts = attemptsResult.Data!.Where(a => a.Id == q.Id).ToList();
                var isResumable = quizAttempts.Any(a => a.Status == AttemptStatus.InProgress);
                var finishedCount = quizAttempts.Count(a =>
                    a.Status == AttemptStatus.Submitted || a.Status == AttemptStatus.TimedOut);
                var canAttempt = isResumable || q.MaxAttempts == null || finishedCount < q.MaxAttempts;

                return new QuizSummaryResponseDto(q.Id, q.Title, q.DurationMinutes, q.PassScore, canAttempt, isResumable);
            }).ToList();

            var response = new DiplomaDetailResponseDto(
                diplomaResult.Data!.Id, diplomaResult.Data.Title, diplomaResult.Data.Description, quizSummaries);

            return RequestResponse<DiplomaDetailResponseDto>.Ok(response);
        }
    }
}
