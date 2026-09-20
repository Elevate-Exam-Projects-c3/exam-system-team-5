using exam_system.Common.Middleware;
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
    public class GetOptionByIdQueryHandler : IRequestHandler<GetOptionByIdQuery, RequestResponse<OptionDto>>
    {
        private readonly IGenericRepository<QuestionOption> _optionRepository;

        public GetOptionByIdQueryHandler(IGenericRepository<QuestionOption> optionRepository)
        {
            _optionRepository = optionRepository;
        }
        public async Task<RequestResponse<OptionDto>> Handle(GetOptionByIdQuery request, CancellationToken cancellationToken)
        {
            var optionDto = await _optionRepository.GetAll().
                Where(o => o.Id == request.OptionId && !o.IsDeleted).ProjectToType<OptionDto>().FirstOrDefaultAsync(cancellationToken);
            if (optionDto is null)
                throw new NotFoundException($"Option with ID {request.OptionId} not found.");

            return RequestResponse<OptionDto>.Ok(optionDto, "Option found");
        }
    }
}
