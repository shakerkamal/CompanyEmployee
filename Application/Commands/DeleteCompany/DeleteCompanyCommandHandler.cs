using AutoMapper;
using Contracts;
using Entities.Exceptions;
using MediatR;

namespace Application.Commands.DeleteCompany;

internal sealed class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public DeleteCompanyCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }
    public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _repositoryManager.Company.GetCompanyAsync(request.Id, request.TrackChanges);
        if (company is null) 
            throw new CompanyNotFoundException(request.Id);

        _repositoryManager.Company.DeleteCompany(company);
        await _repositoryManager.SaveAsync();
    }
}
