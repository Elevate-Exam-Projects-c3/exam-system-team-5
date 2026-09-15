using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.Dtos;
using exam_system.Features.Quizzes.AdminQuizPublishCheck.Queries;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Quizzes.AdminQuizPublishCheck.Handlers
{
    public class GetQuestionOptionsQueryHandler
        : IRequestHandler<GetQuestionOptionsQuery, RequestResponse<List<GetQuestionOptionsResponse>>>
    {
        private readonly IGenericRepository<QuestionOption> _optionRepository;
        private readonly IMapper _mapper;

        public GetQuestionOptionsQueryHandler(
            IGenericRepository<QuestionOption> optionRepository,
            IMapper mapper)
        {
            _optionRepository = optionRepository;
            _mapper = mapper;
        }

        public async Task<RequestResponse<List<GetQuestionOptionsResponse>>> Handle(
            GetQuestionOptionsQuery request,
            CancellationToken cancellationToken)
        {
            var options = await _optionRepository
                .Get(option => request.QuestionIds.Contains(option.QuestionId)
                               && !option.IsDeleted)
                .ToListAsync(cancellationToken);

            return RequestResponse<List<GetQuestionOptionsResponse>>.Ok(
                _mapper.Map<List<GetQuestionOptionsResponse>>(options)
            );
        }
    }
}
