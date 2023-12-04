using MediatR;
using Shared.DataTransferObjects;

namespace Application.Commands.UpdateCompany
{
    public sealed record UpdateCompanyCommand(Guid Id, CompanyUpdateDto CompanyUpdate, bool TrackChanges) : IRequest;
}
