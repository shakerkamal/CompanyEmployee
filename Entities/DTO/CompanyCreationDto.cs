using System.Collections.Generic;

namespace Entities.DTO
{
    public class CompanyCreationDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }

        public IEnumerable<EmployeeCreationDto> Employees { get; set; }
    }
}
