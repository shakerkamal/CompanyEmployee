using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class EmployeeRepositoryExtensions
    {
        public static IQueryable<Employee> FilterEmployees(this IQueryable<Employee> employess, uint minAge, uint maxAge) =>
            employess.Where(e => (e.Age >= minAge && e.Age <= maxAge));

        public static IQueryable<Employee> SearchEmployess(this IQueryable<Employee> employees, string searchTerm)
        {
            if(string.IsNullOrEmpty(searchTerm))
                return employees;
            return employees.Where(e => e.Name.Contains(searchTerm));
        }
    }
}
