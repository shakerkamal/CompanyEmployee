using AutoMapper;
using Contracts;
using Entities.Exceptions;
using MediatR;

namespace Application.Commands.UpdateCompany;

internal sealed class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand>
{
    private readonly IRepositoryManager _repositoryManager; 
    private readonly IMapper _mapper;

    public UpdateCompanyCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }
    public async Task Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var companyEntity = await _repositoryManager.Company.GetCompanyAsync(request.Id, request.TrackChanges); 
        if (companyEntity is null) 
            throw new CompanyNotFoundException(request.Id);
        
        _mapper.Map(request.CompanyUpdate, companyEntity); 
        await _repositoryManager.SaveAsync();
    }
}
