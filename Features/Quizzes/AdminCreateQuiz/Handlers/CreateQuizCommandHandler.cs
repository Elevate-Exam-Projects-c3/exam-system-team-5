using exam_system.Domain.Entities.Diplomas;
using exam_system.Domain.Entities.Quizzes;
using exam_system.Features.Quizzes.AdminCreateQuiz.Commands;
using exam_system.Features.Shared;
using exam_system.Features.Shared.DTOs;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Quizzes.AdminCreateQuiz.Handlers
{
    public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand,  RequestResponse<bool>>
    {
        private readonly IGenericRepository<Quiz> _quizRepository;
        public CreateQuizCommandHandler(IGenericRepository<Quiz> quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<RequestResponse<bool>> Handle(CreateQuizCommand request, CancellationToken cancellationToken=default)
        {
            var quiz = new Quiz             
            {
                Title = request.Title,
                DiplomaId = request.DiplomaID,
                Instructions = request.Instructions,
                DurationMinutes = request.DurationMinutes,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                PassScore = request.PassScore,
                MaxAttempts = request.MaxAttempts,
            };

            await _quizRepository.AddAsync(quiz);

            return RequestResponse<bool>.Created(true, "Quiz created successfully");
        }
    }
}
