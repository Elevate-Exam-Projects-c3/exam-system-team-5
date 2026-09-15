using exam_system.Common.Middleware;
using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.Dtos;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.Queries;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MapsterMapper;
using MediatR;

namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Handlers
{
    public class GetQuizByIdQueryHandler
        : IRequestHandler<GetQuizByIdQuery, RequestResponse<GetQuizByIdResponse>>
    {
        private readonly IGenericRepository<Quiz> _quizRepository;
        private IMapper _mapper;

        public GetQuizByIdQueryHandler(
            IGenericRepository<Quiz> quizRepository,IMapper mapper)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
        }

        public async Task<RequestResponse<GetQuizByIdResponse>> Handle(
            GetQuizByIdQuery request,
            CancellationToken cancellationToken)
        {
            var quiz = await _quizRepository.GetByIdAsync(
                request.QuizId);

            if (quiz == null)
            {
                throw new NotFoundException(
                    "Quiz not found.");
            }

            return RequestResponse<GetQuizByIdResponse>.Ok(
                _mapper.Map<GetQuizByIdResponse>(quiz),
                "Quiz retrieved successfully."
            );
        }
    }
}
