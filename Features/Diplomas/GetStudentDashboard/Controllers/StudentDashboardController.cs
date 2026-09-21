using exam_system.Common.CurrentUser;
using exam_system.Features.Diplomas.GetStudentDashboard.Orchestrators;
using exam_system.Features.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Diplomas.GetStudentDashboard.Controllers
{
    [ApiController]
    [Route("api/students/me")]
    //[Authorize(Roles = "Student")]
    public class StudentDashboardController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUser _currentUser;
        public StudentDashboardController(IMediator mediator , ICurrentUser currentUser)
        {
            _mediator = mediator;
            _currentUser = currentUser;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetStudentDashboardOrchestrator(_currentUser.UserId), cancellationToken);
            var response = EndpointResponse<StudentDashboardResponseViewModel>.FromResult(result);
            return StatusCode(response.StatusCode, response);
        }

    }
}
