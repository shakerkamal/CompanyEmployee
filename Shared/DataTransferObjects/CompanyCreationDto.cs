namespace Shared.DataTransferObjects
{
    public record CompanyCreationDto(string Name, string Address, string Country,
        IEnumerable<EmployeeCreationDto> Employees);
}
