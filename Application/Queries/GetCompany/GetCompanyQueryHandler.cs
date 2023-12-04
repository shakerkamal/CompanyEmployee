using AutoMapper;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Queries.GetCompany;

internal sealed class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, CompanyDto>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public GetCompanyQueryHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<CompanyDto> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
    {
        var company = await _repositoryManager.Company.GetCompanyAsync(request.Id, request.TrackChanges);
        if(company is null)
            throw new CompanyNotFoundException(request.Id);

        var response = _mapper.Map<CompanyDto>(company);
        return response;
    }
}
