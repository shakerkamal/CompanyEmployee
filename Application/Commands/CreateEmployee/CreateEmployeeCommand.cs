using MediatR;
using Shared.DataTransferObjects;

namespace Application.Commands.CreateEmployee;

public sealed record CreateEmployeeCommand(Guid CompanyId, EmployeeCreationDto Employee, bool TrackChanges) : IRequest<EmployeeDto>;

