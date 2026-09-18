using exam_system.Features.Diplomas.AdminUpdateDiploma.Commands;
using exam_system.Features.Diplomas.AdminUpdateDiploma.Controllers.ViewModels;
using exam_system.Features.Shared;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Diplomas.AdminUpdateDiploma.Controllers
{
    [ApiController]
    [Route("api/admin/diplomas")]
    //[Authorize(Roles = "Admin")]r]
    public class UpdateDiplomaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UpdateDiplomaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id , [FromBody] UpdateDiplomaViewModel request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateDiplomaCommand(id, request.Title,request.Description), cancellationToken);
            var response = EndpointResponse<RequestResponse<Unit>>.FromResult(RequestResponse<RequestResponse<Unit>>.Ok(result));
            return StatusCode(response.StatusCode, response);
        }
    }
}
