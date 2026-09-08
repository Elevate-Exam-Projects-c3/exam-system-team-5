using exam_system.Features.Diplomas.AdminUpdateDiploma.Commands;
using exam_system.Features.Shared;
using MediatR;

namespace exam_system.Features.Diplomas.AdminUpdateDiploma.Handlers
{
    public class UpdateDiplomaCommandHandler : IRequestHandler<UpdateDiplomaCommand , RequestResponse<Unit>>
    {
        private readonly IMediator _mediator;
        public UpdateDiplomaCommandHandler( IMediator mediator) 
        { 
            _mediator = mediator;
        
        }

        public Task<RequestResponse<Unit>> Handle(UpdateDiplomaCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
