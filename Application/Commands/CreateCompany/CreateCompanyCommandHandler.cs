using AutoMapper;
using Contracts;
using Entities.Models;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Commands.CreateCompany;

internal sealed class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public CreateCompanyCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }
    public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var companyEntity = _mapper.Map<Company>(request.Company);

        await _repositoryManager.Company.CreateCompanyAsync(companyEntity);
        await _repositoryManager.SaveAsync();

        var result = _mapper.Map<CompanyDto>(companyEntity);
        return result;
    }
}
