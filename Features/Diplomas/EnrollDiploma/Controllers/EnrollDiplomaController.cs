using exam_system.Common.CurrentUser;
using exam_system.Features.Diplomas.EnrollDiploma.Commands;
using exam_system.Features.Diplomas.EnrollDiploma.Orchestrators;
using exam_system.Features.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Diplomas.EnrollDiploma.Controllers
{
    [ApiController]
    [Route("api/diplomas")]
    //[Authorize(Roles = "Student")]
    public class EnrollDiplomaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUser _currentUser;
        public EnrollDiplomaController(IMediator mediator , ICurrentUser currentUser)
        {
            _mediator = mediator;
            _currentUser = currentUser;
        }

        [HttpPost("{diplomaId:guid}/enroll")]
        public async Task<IActionResult> Enroll(Guid diplomaId, CancellationToken cancellationToken)
        {
            //test
            //Guid studentId = new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa");

            var result = await _mediator.Send(new EnrollInDiplomaOrchestrator(_currentUser.UserId, diplomaId), cancellationToken);
            var response = EndpointResponse<Unit>.FromResult(result);
            return StatusCode(response.StatusCode, response);
        }
    }
}
