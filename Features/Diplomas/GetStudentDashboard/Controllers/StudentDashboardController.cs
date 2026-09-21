using exam_system.Common.CurrentUser;
using exam_system.Features.Diplomas.GetStudentDashboard.Controllers.ViewModels;
using exam_system.Features.Diplomas.GetStudentDashboard.Orchestrators;
using exam_system.Features.Shared;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            //test
            var userId = new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa");

            //var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var dashboardResult = await _mediator.Send(new GetStudentDashboardOrchestrator(userId), cancellationToken);

            var dashboardViewModel = dashboardResult.Data.Adapt<StudentDashboardResponseViewModel>();

            var response = EndpointResponse<StudentDashboardResponseViewModel>
                .FromResult(RequestResponse<StudentDashboardResponseViewModel>.Ok(dashboardViewModel));
            return StatusCode(response.StatusCode, response);
        }

    }
}
