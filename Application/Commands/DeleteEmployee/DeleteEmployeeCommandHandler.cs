using AutoMapper;
using Contracts;
using Entities.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.DeleteEmployee;

internal sealed class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public DeleteEmployeeCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }
    public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var company = await _repositoryManager.Company.GetCompanyAsync(request.CompanyId, request.TrackChanges);
        if (company is null)
            throw new CompanyNotFoundException(request.CompanyId);

        var employeeForCompany = await _repositoryManager.Employee.GetEmployeeAsync(request.CompanyId, request.Id, request.TrackChanges);
        if (employeeForCompany is null)
            throw new EmployeeNotFoundException(request.Id);

        _repositoryManager.Employee.DeleteEmployee(employeeForCompany);
        await _repositoryManager.SaveAsync();
    }
}
