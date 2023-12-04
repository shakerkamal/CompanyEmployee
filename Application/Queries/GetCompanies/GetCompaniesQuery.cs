using MediatR;
using Shared.DataTransferObjects;

namespace Application.Queries.GetCompanies;

public sealed record GetCompaniesQuery(bool TrackChanges) : IRequest<IEnumerable<CompanyDto>>;
