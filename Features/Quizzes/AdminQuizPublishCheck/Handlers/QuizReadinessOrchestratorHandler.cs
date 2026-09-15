using exam_system.Features.Quizzes.AdminQuizPublishCheck.Dtos;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.Orchestrators;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.Queries;
using MapsterMapper;
using MediatR;

namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Handlers
{
    public class QuizReadinessOrchestratorHandler
        : IRequestHandler<QuizReadinessOrchestrator, QuizReadinessResponse>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public QuizReadinessOrchestratorHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<QuizReadinessResponse> Handle(
            QuizReadinessOrchestrator request,
            CancellationToken cancellationToken)
        {
            var quiz = await _mediator.Send(
                new GetQuizByIdQuery(request.QuizId),
                cancellationToken);

            var questions = await _mediator.Send(
                new GetQuizQuestionsQuery(request.QuizId),
                cancellationToken);

            var questionIds = questions
                .Select(q => q.QuestionId)
                .ToList();

            var options = await _mediator.Send(
                new GetQuestionOptionsQuery(questionIds),
                cancellationToken);

            var checks = new List<ReadinessCheck>();

            // 1. At least one question
            var hasQuestions = questions.Any();

            checks.Add(new ReadinessCheck
            {
                Name = "Questions",
                Passed = hasQuestions,
                Message = hasQuestions
                    ? "At least one question exists."
                    : "Quiz must have at least one question."
            });

            // 2. Every question has exactly one correct option
            var everyQuestionHasOneCorrectOption =
                questions.All(question =>
                    options.Count(option =>
                        option.QuestionId == question.QuestionId &&
                        option.IsCorrect) == 1);

            checks.Add(new ReadinessCheck
            {
                Name = "Correct Options",
                Passed = everyQuestionHasOneCorrectOption,
                Message = everyQuestionHasOneCorrectOption
                    ? "Every question has exactly one correct option."
                    : "Every question must have exactly one correct option."
            });

            // 3. Duration
            var validDuration = quiz.DurationMinutes > 0;

            checks.Add(new ReadinessCheck
            {
                Name = "Duration",
                Passed = validDuration,
                Message = validDuration
                    ? "Duration is valid."
                    : "Duration must be greater than 0."
            });

            // 4. Pass Score
            var validPassScore =
                quiz.PassScore >= 0 &&
                quiz.PassScore <= 100;

            checks.Add(new ReadinessCheck
            {
                Name = "Pass Score",
                Passed = validPassScore,
                Message = validPassScore
                    ? "Pass score is valid."
                    : "Pass score must be between 0 and 100."
            });

            return new QuizReadinessResponse
            {
                QuizId = quiz.QuizId,
                IsReady = checks.All(check => check.Passed),
                Checks = checks
            };
        }
    }
}
