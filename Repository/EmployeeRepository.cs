using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public void CreateEmployee(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }

        public Employee GetEmployee(Guid companyId, Guid id, bool trackChanges)
        {
            return FindByCondition(e => e.CompanyId == companyId && e.Id == id, trackChanges)
                .FirstOrDefault();
        }

        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges)
        {
            return FindByCondition(e => e.CompanyId == companyId, trackChanges)
                .OrderBy(e => e.Name);
        }

        public void UpdateEmployee(Employee employee)
        {
            Update(employee);
        }
    }
}
