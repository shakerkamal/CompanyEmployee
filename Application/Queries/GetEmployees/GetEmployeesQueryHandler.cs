using AutoMapper;
using Contracts;
using Entities.Exceptions;
using MediatR;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Application.Queries.GetEmployees;

internal sealed class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, (IEnumerable<EmployeeDto> employees, MetaData metaData)>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public GetEmployeesQueryHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }
    public async Task<(IEnumerable<EmployeeDto> employees, MetaData metaData)> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        if (!request.Parameters.ValidAgeRange)
            throw new MaxAgeRangeException();
        var company = await _repositoryManager.Company.GetCompanyAsync(request.CompanyId, request.TrackChanges);
        if (company is null)
            throw new CompanyNotFoundException(request.CompanyId);

        var employeesWithMetaData = await _repositoryManager.Employee
            .GetEmployeesAsync(request.CompanyId, request.Parameters, request.TrackChanges);

        var result = _mapper.Map<IEnumerable<EmployeeDto>>(employeesWithMetaData);
        return (employees: result, metaData: employeesWithMetaData.MetaData);
    }
}
