using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminManageQuestions.Commands;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Quizzes.AdminManageQuestions.Handlers
{
    public class UpdateOptionsCommandHandler
        : IRequestHandler<UpdateOptionsCommand, Unit>
    {
        private readonly IGenericRepository<QuestionOption> _optionRepository;

        public UpdateOptionsCommandHandler(
            IGenericRepository<QuestionOption> optionRepository)
        {
            _optionRepository = optionRepository;
        }

        public async Task<Unit> Handle(
            UpdateOptionsCommand request,
            CancellationToken cancellationToken)
        {
            IQueryable<QuestionOption> query =
                _optionRepository.Get(x => x.QuestionId == request.QuestionId);

            var oldOptions = await EntityFrameworkQueryableExtensions
                .ToListAsync(query, cancellationToken);

            foreach (var option in oldOptions)
            {
                _optionRepository.HardDelete(option);
            }

            var newOptions = request.Options.Select(option =>
                new QuestionOption
                {
                    QuestionId = request.QuestionId,
                    OptionText = option.OptionText,
                    IsCorrect = option.IsCorrect
                });

            await _optionRepository.AddRangeAsync(newOptions);

            return Unit.Value;
        }
    }
}
