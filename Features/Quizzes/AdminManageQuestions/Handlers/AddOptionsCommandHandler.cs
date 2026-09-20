using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminManageQuestions.Commands;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Quizzes.AdminManageQuestions.Handlers
{
    public class AddOptionsCommandHandler : IRequestHandler<AddOptionsCommand, Unit>
    {
        private readonly IGenericRepository<QuestionOption> _optionRepository;

        public AddOptionsCommandHandler(
            IGenericRepository<QuestionOption> optionRepository)
        {
            _optionRepository = optionRepository;
        }

        public async Task<Unit> Handle(
            AddOptionsCommand request,
            CancellationToken cancellationToken)
        {
            var options = request.Options.Select(option => new QuestionOption
            {
                QuestionId = request.QuestionId,
                OptionText = option.OptionText,
                IsCorrect = option.IsCorrect
            });

            await _optionRepository.AddRangeAsync(options);

            return Unit.Value;
        }
    }
}
