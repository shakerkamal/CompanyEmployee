using AutoMapper;
using Contracts;
using Entities.Exceptions;
using MediatR;

namespace Application.Commands.DeleteCompany;

internal sealed class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
{
    private readonly IRepositoryManager _repositoryManager;

    public DeleteCompanyCommandHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
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
