using MediatR;

namespace Application.Commands.DeleteCompany;


public sealed record DeleteCompanyCommand(Guid Id, bool TrackChanges) : IRequest;
