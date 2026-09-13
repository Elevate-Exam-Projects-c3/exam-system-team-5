using exam_system.Features.Diplomas.AdminDeleteDiploma.Commands;
using exam_system.Features.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Diplomas.AdminDeleteDiploma.Controllers
{
    [ApiController]
    [Route("api/admin/diplomas")]
    //[Authorize(Roles = "Admin")]
    public class DeleteDiplomaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DeleteDiplomaController(IMediator mediator )
        {
            _mediator = mediator;
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteDiplomaCommand(id), cancellationToken);
            var response = EndpointResponse<RequestResponse<Unit>>.FromResult(RequestResponse<RequestResponse<Unit>>.Ok(result));
            return StatusCode(response.StatusCode, response);
        }
    }
}
