using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.AdminCreateDiploma.Commands;
using exam_system.Features.Diplomas.AdminCreateDiploma.Controllers.ViewModels;
using exam_system.Features.Shared;
using exam_system.Migrations;
using exam_system.Persistence.DataAccess;
using Mapster;
using MediatR;

namespace exam_system.Features.Diplomas.AdminCreateDiploma.Handlers
{
    public class CreateDiplomaCommandHandler : IRequestHandler<CreateDiplomaCommand, RequestResponse<Unit>>
    {
        private readonly IGenericRepository<Diploma> _diplomaRepository;

        public CreateDiplomaCommandHandler(IGenericRepository<Diploma> diplomaRepository)
        {
            _diplomaRepository = diplomaRepository;
        }

        public async Task<RequestResponse<Unit>> Handle(CreateDiplomaCommand request, CancellationToken cancellationToken)
        {
            var diploma = new Diploma 
            { 
                Title = request.Title, 
                Description = request.Description 
            };
            
            //not need addasync -- it just chage entry status to be add
            _diplomaRepository.Add(diploma);
            return RequestResponse<Unit>.Created(Unit.Value);


        }
    }
}
