using exam_system.Common.Middleware;
using exam_system.Features.Attempts.SubmitQuestionAnswer.Queries;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Attempts.SubmitQuestionAnswer.Handlers
{
    public class CheckDeadlineQueryHandler : IRequestHandler<CheckDeadlineQuery, RequestResponse<bool>>
    {
        public Task<RequestResponse<bool>> Handle(CheckDeadlineQuery request, CancellationToken cancellationToken)
        {
            if (request.Deadline < DateTime.UtcNow)
            {
                throw new GoneException("Deadline has passed.");
                //return Task.FromResult(RequestResponse<bool>.Fail("Deadline has passed.", 410));
            }
            else
            {
                return Task.FromResult(RequestResponse<bool>.Ok(false, "Deadline is still valid."));
            }

        }
    }
}
