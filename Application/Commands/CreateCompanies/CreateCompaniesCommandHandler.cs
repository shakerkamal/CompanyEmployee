using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Commands.CreateCompanies;

internal sealed class CreateCompaniesCommandHandler : IRequestHandler<CreateCompaniesCommand, (IEnumerable<CompanyDto> companies, string ids)>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public CreateCompaniesCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }
    public async Task<(IEnumerable<CompanyDto> companies, string ids)> Handle(CreateCompaniesCommand request, CancellationToken cancellationToken)
    {
        if (request.Companies is null)
            throw new CompanyCollectionBadRequest();

        var companyEntities = _mapper.Map<IEnumerable<Company>>(request.Companies);

        foreach (var company in companyEntities)
        {
            await _repositoryManager.Company.CreateCompanyAsync(company);
        }
        await _repositoryManager.SaveAsync();

        var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
        var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));

        return (companies: companyCollectionToReturn, ids: ids);
    }
}
