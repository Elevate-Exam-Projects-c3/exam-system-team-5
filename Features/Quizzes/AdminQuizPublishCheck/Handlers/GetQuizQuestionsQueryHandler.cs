using exam_system.Domain.Entities.Quizzes;
using Microsoft.EntityFrameworkCore;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.Dtos;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.Queries;
using exam_system.Persistence.DataAccess;
using MediatR;
using MapsterMapper;
using exam_system.Features.Shared;

namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Handlers
{
    public class GetQuizQuestionsQueryHandler
        : IRequestHandler<GetQuizQuestionsQuery, RequestResponse<List<GetQuizQuestionsResponse>>>
    {
        private readonly IGenericRepository<Question> _questionRepository;
        private IMapper _mapper;

        public GetQuizQuestionsQueryHandler(
            IGenericRepository<Question> questionRepository,IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<RequestResponse<List<GetQuizQuestionsResponse>>> Handle(
            GetQuizQuestionsQuery request,
            CancellationToken cancellationToken)
        {
            var questions = await _questionRepository
                .Get(q => q.QuizId == request.QuizId && !q.IsDeleted)
                .ToListAsync(cancellationToken);

            return RequestResponse<List<GetQuizQuestionsResponse>>.Ok(
                questions
                    .Select(q => _mapper.Map<GetQuizQuestionsResponse>(q))
                    .ToList(),
                "Quiz questions retrieved successfully."
            );
        }
    }
}
