using exam_system.Features.Diplomas.BrowseDiplomas.Controllers.ViewModels;
using exam_system.Features.Diplomas.GetDiplomaDetail.Controllers.ViewModel;
using exam_system.Features.Diplomas.GetDiplomaDetail.Orchestrators;
using exam_system.Features.Diplomas.GetDiplomaDetail.Queries;
using exam_system.Features.Shared;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam_system.Features.Diplomas.GetDiplomaDetail.Controllers
{
    [ApiController]
    [Route("api/diplomas")]
    //[Authorize(Roles = "Student")]
    public class GetDiplomaDetailController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetDiplomaDetailController(IMediator mediator) => _mediator = mediator;

        [HttpGet("{diplomaId:guid}")]
        public async Task<IActionResult> GetById(Guid diplomaId, CancellationToken cancellationToken)
        {
            var studentId = new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa");
            var result = await _mediator.Send(new GetDiplomaDetailOrchestrator(diplomaId, studentId), cancellationToken);

            //map
            var diplomaResult = result.Data.Adapt<DiplomaDetailResponseViewModel>();

            var diplomaDetailsResponse = EndpointResponse<DiplomaDetailResponseViewModel>
               .FromResult(RequestResponse<DiplomaDetailResponseViewModel>.Ok(diplomaResult));

            return StatusCode(diplomaDetailsResponse.StatusCode, diplomaDetailsResponse);
        }
    }
}
