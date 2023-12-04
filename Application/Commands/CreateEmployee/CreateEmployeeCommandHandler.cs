using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Commands.CreateEmployee;

internal sealed class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public CreateEmployeeCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }
    public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var company = await _repositoryManager.Company.GetCompanyAsync(request.CompanyId, request.TrackChanges);
        if (company is null)
            throw new CompanyNotFoundException(request.CompanyId);

        var employeeEntity = _mapper.Map<Employee>(request.Employee);

        await _repositoryManager.Employee.CreateEmployeeAsync(request.CompanyId, employeeEntity);
        await _repositoryManager.SaveAsync();

        var result = _mapper.Map<EmployeeDto>(employeeEntity);
        return result;
    }
}
