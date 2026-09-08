using Azure;
using exam_system.Features.Diplomas.AdminCreateDiploma.Commands;
using exam_system.Features.Diplomas.AdminCreateDiploma.Commands.ViewModels;
using exam_system.Features.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Diplomas.AdminCreateDiploma.Controllers
{
    [ApiController]
    [Route("api/admin/diplomas")]
    //[Authorize(Roles = "Admin")]
    public class CreateDiplomaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CreateDiplomaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDiplomaRequestViewModel request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateDiplomaCommand(request.Title, request.Description), cancellationToken);
            var response = EndpointResponse<RequestResponse<Unit>>.FromResult(RequestResponse<RequestResponse<Unit>>.Ok(result));
            return StatusCode(response.StatusCode, response);

        }
    }
}
