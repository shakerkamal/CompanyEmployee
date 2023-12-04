using MediatR;

namespace Application.Commands.DeleteEmployee;

public sealed record DeleteEmployeeCommand(Guid CompanyId, Guid Id, bool TrackChanges) : IRequest;
