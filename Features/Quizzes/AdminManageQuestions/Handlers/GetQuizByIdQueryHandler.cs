using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminManageQuestions.Dtos;
using exam_system.Features.Quizzes.AdminManageQuestions.Queries;
using exam_system.Persistence.DataAccess;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace exam_system.Features.Quizzes.AdminManageQuestions.Handlers
{
    public class GetQuizByIdQueryHandler
        : IRequestHandler<GetQuizByIdQuery, GetQuizByIdResponse>
    {
        private readonly IGenericRepository<Quiz> _quizRepository;
        private IMapper _mapper;

        public GetQuizByIdQueryHandler(
            IGenericRepository<Quiz> quizRepository,IMapper mapper)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
        }

        public async Task<GetQuizByIdResponse> Handle(
            GetQuizByIdQuery request,
            CancellationToken cancellationToken)
        {
            var quiz = await _quizRepository.GetByIdAsync(request.QuizId);

            var quizResponse = _mapper.Map<GetQuizByIdResponse>(quiz);

            return quizResponse;
        }
    }
}

            