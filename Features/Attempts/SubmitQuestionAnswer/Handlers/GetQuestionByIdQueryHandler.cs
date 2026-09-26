using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Attempts.SubmitQuestionAnswer.Dtos;
using exam_system.Features.Attempts.SubmitQuestionAnswer.Queries;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Handlers
{
    public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, RequestResponse<QuestionDto>>
    {
        private readonly IGenericRepository<Question> _questionRepository;

        public GetQuestionByIdQueryHandler(IGenericRepository<Question> questionRepository)
        {
            _questionRepository = questionRepository;
        }
        public async Task<RequestResponse<QuestionDto>> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var questionDto =await _questionRepository.GetAll().Where
                (q => q.Id == request.QuestionId && !q.IsDeleted).ProjectToType<QuestionDto>().FirstOrDefaultAsync(cancellationToken);

            if (questionDto is null)
                return RequestResponse<QuestionDto>.Fail("Question not found", 404);

            return RequestResponse<QuestionDto>.Ok(questionDto, "success question found");
        }
    }
}
