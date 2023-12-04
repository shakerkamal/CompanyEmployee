using MediatR;
using Shared.DataTransferObjects;

namespace Application.Commands.CreateCompanies;

public sealed record CreateCompaniesCommand(IEnumerable<CompanyCreationDto> Companies) : IRequest<(IEnumerable<CompanyDto> companies, string ids)>;
