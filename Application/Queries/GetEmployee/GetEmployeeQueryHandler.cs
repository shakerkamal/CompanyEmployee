using AutoMapper;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Queries.GetEmployee;

internal sealed class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, EmployeeDto>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public GetEmployeeQueryHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }
    public async Task<EmployeeDto> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var company = await _repositoryManager.Company.GetCompanyAsync(request.CompanyId, request.TrackChanges);
        if (company is null)
            throw new CompanyNotFoundException(request.CompanyId);

        var employeeEntity = await _repositoryManager.Employee.GetEmployeeAsync(request.CompanyId, request.EmployeeId, request.TrackChanges);
        if (employeeEntity is null)
            throw new EmployeeNotFoundException(request.EmployeeId);

        var result = _mapper.Map<EmployeeDto>(employeeEntity);
        return result;
    }
}
