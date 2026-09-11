
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Diplomas.BrowseDiplomas.Controllers.ViewModels;
using exam_system.Features.Diplomas.BrowseDiplomas.Dtos;
using exam_system.Features.Diplomas.BrowseDiplomas.Queries;
using exam_system.Features.Shared;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace exam_system.Features.Diplomas.BrowseDiplomas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Student")]
    public class BrowseDiplomasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrowseDiplomasController(IMediator mediator ) 
        { 
           _mediator = mediator;
        }

        [HttpGet("GetStudentDiploma")]
        public async Task<IActionResult> GetStudentDiploma([FromQuery] DiplomaItemsRequestViewModel request, CancellationToken cancellationToken = default)
        {
            //test
            Guid studentId = new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa");


            var getDiplomasQueryResult = await _mediator.Send(new GetDiplomasQuery(studentId, request.PageIndex, request.PageSize),cancellationToken);

            if (!getDiplomasQueryResult.Success)
            {
                var failResponse = EndpointResponse<PaginatedResult<DiplomaItemsResponseViewModel>>
                    .FromResult(RequestResponse<PaginatedResult<DiplomaItemsResponseViewModel>>
                        .Fail(getDiplomasQueryResult.Message, getDiplomasQueryResult.StatusCode));
                return Ok(failResponse);
            }

            // Map using Mapster - properties have same names, no configuration needed!
            var diplomasData = getDiplomasQueryResult.Data.Adapt<PaginatedResult<DiplomaItemsResponseViewModel>>();

            var response = EndpointResponse<PaginatedResult<DiplomaItemsResponseViewModel>>
                .FromResult(RequestResponse<PaginatedResult<DiplomaItemsResponseViewModel>>
                    .Ok(diplomasData));

            return Ok(response);
        }

    }
}

