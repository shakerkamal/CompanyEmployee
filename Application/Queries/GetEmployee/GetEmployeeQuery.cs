using MediatR;
using Shared.DataTransferObjects;

namespace Application.Queries.GetEmployee;

public sealed record GetEmployeeQuery(Guid CompanyId, Guid EmployeeId, bool TrackChanges) : IRequest<EmployeeDto>;
