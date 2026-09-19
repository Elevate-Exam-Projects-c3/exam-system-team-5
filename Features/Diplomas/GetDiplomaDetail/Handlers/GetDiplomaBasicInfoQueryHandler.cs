using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.GetDiplomaDetail.Dtos;
using exam_system.Features.Diplomas.GetDiplomaDetail.Queries;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Diplomas.GetDiplomaDetail.Handlers
{
    public class GetDiplomaBasicInfoQueryHandler : IRequestHandler<GetDiplomaBasicInfoQuery, RequestResponse<DiplomaBasicInfoDto>>
    {
        private readonly IGenericRepository<Diploma> _diplomaRepository;

        public GetDiplomaBasicInfoQueryHandler(IGenericRepository<Diploma> diplomaRepository)
        {
            _diplomaRepository = diplomaRepository;
        }

        public async Task<RequestResponse<DiplomaBasicInfoDto>> Handle(GetDiplomaBasicInfoQuery request, CancellationToken cancellationToken)
        {
            var diploma = await _diplomaRepository
                .Get(d => d.Id == request.DiplomaId)
                .ProjectToType<DiplomaBasicInfoDto>()
                .FirstOrDefaultAsync(cancellationToken);

            return diploma is null
                ? RequestResponse<DiplomaBasicInfoDto>.Fail("Diploma not found.", 404)
                : RequestResponse<DiplomaBasicInfoDto>.Ok(diploma);
        }
    }
}
