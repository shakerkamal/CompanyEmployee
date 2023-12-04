using AutoMapper;
using Contracts;
using Entities.Exceptions;
using MediatR;

namespace Application.Commands.UpdateEmployee;

internal sealed class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public UpdateEmployeeCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }
    public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var company = await _repositoryManager.Company.GetCompanyAsync(request.CompanyId, request.CompTrackChanges);
        if (company is null)
            throw new CompanyNotFoundException(request.CompanyId);

        var employeeForCompany = await _repositoryManager.Employee.GetEmployeeAsync(request.CompanyId, request.Id, request.TrackChanges);
        if (employeeForCompany is null)
            throw new EmployeeNotFoundException(request.Id);

        _mapper.Map(request.EmployeeUpdate, employeeForCompany);
        await _repositoryManager.SaveAsync();
    }
}
