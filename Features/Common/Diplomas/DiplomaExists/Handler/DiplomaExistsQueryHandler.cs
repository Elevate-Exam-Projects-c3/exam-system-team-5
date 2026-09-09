using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Common.Diplomas.DiplomaExists.Query;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace exam_system.Features.Common.Diplomas.DiplomaExists.Handler
{
    public class DiplomaExistsQueryHandler : IRequestHandler<DiplomaExistsQuery , RequestResponse<bool>>
    {
        private readonly IGenericRepository<Diploma> _diplomaRepository;

        public DiplomaExistsQueryHandler(IGenericRepository<Diploma> diplomaRepository) 
        {
            _diplomaRepository = diplomaRepository;
        }

        public async Task<RequestResponse<bool>> Handle(DiplomaExistsQuery query, CancellationToken cancellationToken)
        {
            var isExists =  await _diplomaRepository.ExistsAsync(d => d.Id == query.Id, cancellationToken);
            return RequestResponse<bool>.Ok(isExists);
        }
    }
}
