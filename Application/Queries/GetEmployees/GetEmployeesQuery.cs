using MediatR;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Application.Queries.GetEmployees;

public sealed record GetEmployeesQuery(Guid CompanyId, EmployeeParameters Parameters, bool TrackChanges) : IRequest<(IEnumerable<EmployeeDto> employees, MetaData metaData)>;
