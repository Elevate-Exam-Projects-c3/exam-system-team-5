using exam_system.Domain.Entities.Attempts;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Attempts.SubmitQuestionAnswer.Dtos;
using exam_system.Features.Attempts.SubmitQuestionAnswer.Queries;
using exam_system.Persistence.DataAccess;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Handlers
{
    public class GetAttemptByIdQueryHandler
        : IRequestHandler<GetAttemptByIdQuery, QuizAttemptDto>
    {
        private readonly IGenericRepository<QuizAttempt> _attemptRepository;


        public GetAttemptByIdQueryHandler(
            IGenericRepository<QuizAttempt> attemptRepository
           )
        {
            _attemptRepository = attemptRepository;

        }

        public async Task<QuizAttemptDto> Handle(
            GetAttemptByIdQuery request,
            CancellationToken cancellationToken)
        {
            var attemptResponse = await _attemptRepository
                .GetAll()
                .Where(a => a.Id == request.AttemptId&&!a.IsDeleted)
                .ProjectToType<QuizAttemptDto>()
                .FirstOrDefaultAsync(cancellationToken);

            if (attemptResponse == null)
            {
                throw new KeyNotFoundException($"Attempt with ID {request.AttemptId} not found.");
            }

            return attemptResponse;
        }
    }
}
