using MediatR;
using Shared.DataTransferObjects;

namespace Application.Commands.CreateCompany;

public sealed record CreateCompanyCommand(CompanyCreationDto Company) : IRequest<CompanyDto>;
