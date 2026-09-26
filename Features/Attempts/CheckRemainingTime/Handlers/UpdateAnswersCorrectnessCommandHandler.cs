using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.CheckRemainingTime.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace exam_system.Features.Attempts.CheckRemainingTime.Handlers
{
    public class UpdateAnswersCorrectnessCommandHandler : IRequestHandler<UpdateAnswersCorrectnessCommand, RequestResponse<Unit>>
    {
        private readonly IGenericRepository<StudentQuestionAnswer> _answerRepository;
        public UpdateAnswersCorrectnessCommandHandler(IGenericRepository<StudentQuestionAnswer> answerRepository)
            => _answerRepository = answerRepository;
        public async Task<RequestResponse<Unit>> Handle(UpdateAnswersCorrectnessCommand command, CancellationToken cancellationToken)
        {
            foreach (var answer in command.Answers)
                await _answerRepository.UpdateAsync(
                    a => a.Id == answer.AnswerId,
                    setters => setters.SetProperty(a => a.IsCorrect, answer.IsCorrect),
                    cancellationToken);

            return RequestResponse<Unit>.Ok(Unit.Value);
        }
    }
}
