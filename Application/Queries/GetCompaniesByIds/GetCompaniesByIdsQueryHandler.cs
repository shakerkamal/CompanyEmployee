using AutoMapper;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Queries.GetCompaniesByIds;

internal sealed class GetCompaniesByIdsQueryHandler : IRequestHandler<GetCompaniesByIdsQuery, IEnumerable<CompanyDto>>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public GetCompaniesByIdsQueryHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }
    public async Task<IEnumerable<CompanyDto>> Handle(GetCompaniesByIdsQuery request, CancellationToken cancellationToken)
    {
        if (request.Ids is null)
            throw new IdParametersBadRequestException();

        var companyEntities = await _repositoryManager.Company.GetByIdsAsync(request.Ids, request.TrackChanges);
        if (request.Ids.Count() != companyEntities.Count())
            throw new CollectionByIdsBadRequestException();

        var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
        return companiesToReturn;
    }
}
