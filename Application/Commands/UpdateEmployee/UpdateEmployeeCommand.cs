using MediatR;
using Shared.DataTransferObjects;

namespace Application.Commands.UpdateEmployee;

public sealed record UpdateEmployeeCommand(Guid CompanyId, Guid Id, EmployeeUpdateDto EmployeeUpdate, bool CompTrackChanges, bool TrackChanges) 
    : IRequest;
