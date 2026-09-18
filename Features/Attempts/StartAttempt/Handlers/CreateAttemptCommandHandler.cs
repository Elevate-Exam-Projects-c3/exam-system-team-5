using exam_system.Common.Enums;
using exam_system.Domain.Entities.Attempts;
using exam_system.Features.Attempts.StartAttempt.Commands;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Attempts.StartAttempt.Handlers
{
    public class CreateAttemptCommandHandler(IGenericRepository<QuizAttempt> repository) : IRequestHandler<CreateAttemptCommand, RequestResponse<bool>>
    {
        public async Task<RequestResponse<bool>> Handle(CreateAttemptCommand request, CancellationToken cancellationToken)
        {

            //Chick if the student has already started the quiz and is still in progress
            var InprograssAttempt = repository.GetAll()
                .FirstOrDefault(x => x.QuizId == request.Quiz.Id 
                && x.StudentId == request.StudentId 
                && x.Status == AttemptStatus.InProgress);

            if(InprograssAttempt is not null)
            {
                return RequestResponse<bool>.Ok(true, "You alredy have inprograss attempt");
            }



            //chick if student hit the attempts limt
            if (request.Quiz.MaxAttempts.HasValue) 
            {
                var attemptsCount = repository.GetAll()
                    .Count(x => x.QuizId == request.Quiz.Id
                    && x.StudentId == request.StudentId
                    && (x.Status == AttemptStatus.Submitted ||
                        x.Status == AttemptStatus.TimedOut));

                if (attemptsCount >= request.Quiz.MaxAttempts.Value)
                {
                    return RequestResponse<bool>.Fail("Attempts exhausted");
                }

            }


            //create attempt
            var StartTime = DateTime.UtcNow;
            var attempt = new QuizAttempt
            {
                QuizId = request.Quiz.Id,
                StudentId = request.StudentId,
                StartTime = StartTime,
                Status = AttemptStatus.InProgress,
                Deadline=StartTime.AddMinutes(request.Quiz.DurationMinutes)
            };

            await repository.AddAsync(attempt);
            return RequestResponse<bool>.Created(true, "Quiz created successfully");
        }
    }
}
