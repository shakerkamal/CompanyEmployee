using MediatR;
using Shared.DataTransferObjects;

namespace Application.Queries.GetCompaniesByIds;

public sealed record GetCompaniesByIdsQuery(IEnumerable<Guid> Ids, bool TrackChanges) : IRequest<IEnumerable<CompanyDto>>;
