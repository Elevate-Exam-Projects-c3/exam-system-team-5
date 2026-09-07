using exam_system.Common.Enums;
using exam_system.Domain.Entities.Diplomas;
using exam_system.Features.Diplomas.BrowseDiplomas.Dtos;
using exam_system.Features.Diplomas.BrowseDiplomas.Queries;
using exam_system.Features.Shared;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Diplomas.BrowseDiplomas.Handlers
{

    public class GetDiplomasQueryHandler : IRequestHandler<GetDiplomasQuery, RequestResponse<PaginatedResult<DiplomaItemsResponse>>>
    {
        private readonly IGenericRepository<Diploma> _diplomaRepository;

        public GetDiplomasQueryHandler(IGenericRepository<Diploma> diplomaRepository)
        {
            _diplomaRepository = diplomaRepository;
        }

        public async Task<RequestResponse<PaginatedResult<DiplomaItemsResponse>>> Handle(GetDiplomasQuery request, CancellationToken cancellationToken)
        {
            var diplomas = _diplomaRepository.Get(a => a.Quizzes.Any(q => q.Status == QuizStatus.Published));
            //var totalCount = await diplomas.Count(cancellationToken);

            return RequestResponse<PaginatedResult<DiplomaItemsResponse>>.Ok(diplomas);


        }
    }
}
