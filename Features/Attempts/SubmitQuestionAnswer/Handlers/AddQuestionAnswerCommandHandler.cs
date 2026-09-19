using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.SubmitQuestionAnswer.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Handlers
{
    public class AddQuestionAnswerCommandHandler : IRequestHandler<AddQuestionAnswerCommand, RequestResponse<uint>>
    {
        private IGenericRepository<StudentQuestionAnswer> _studentQuestionAnswerRepository;

        public AddQuestionAnswerCommandHandler(IGenericRepository<StudentQuestionAnswer> studentQuestionAnswerRepository)
        {
            _studentQuestionAnswerRepository = studentQuestionAnswerRepository;
        }
        public async Task<RequestResponse<uint>> Handle(AddQuestionAnswerCommand request, CancellationToken cancellationToken)
        {
            var result = await _studentQuestionAnswerRepository.GetAll().Where
                (x => x.QuestionId == request.QuestionId && x.AttemptId == request.AttemptId).FirstOrDefaultAsync(cancellationToken);


            if (result != null)
            {
                result.SelectedOptionId = request.Option.Id;
                await _studentQuestionAnswerRepository.UpdateAsync(result);
                return RequestResponse<uint>.Ok(1, "Answer updated successfully");
            }
            else
            {
                var studentQuestionAnswer = new StudentQuestionAnswer
                {
                    QuestionId = request.QuestionId,
                    SelectedOptionId = request.Option.Id,
                    AttemptId = request.AttemptId,
                    IsCorrect = request.Option.IsCorrect
                };
                await _studentQuestionAnswerRepository.AddAsync(studentQuestionAnswer);
                return RequestResponse<uint>.Ok(1, "Answer added successfully");
            }

            
        }
    }
}
