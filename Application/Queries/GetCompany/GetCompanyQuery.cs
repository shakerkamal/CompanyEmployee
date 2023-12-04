using MediatR;
using Shared.DataTransferObjects;

namespace Application.Queries.GetCompany;

public sealed record GetCompanyQuery(Guid Id, bool TrackChanges) : IRequest<CompanyDto>;
