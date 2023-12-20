using Entities.Models;
using Moq;
using Repository;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployee.UnitTest;

public class EmployeeRepositoryTests
{
    private readonly Mock<RepositoryContext> _mockDbContext;
    private readonly EmployeeRepository _employeeRepository;

    public EmployeeRepositoryTests()
    {
        _mockDbContext = new Mock<RepositoryContext>();
        _employeeRepository = new EmployeeRepository(_mockDbContext.Object);
    }

    [Fact]
    public async void GetEmployeesAsync_ShouldReturnPaginatedEmployee()
    {
        //Arrange
        //var mockDbContext = new Mock<RepositoryContext>();
        //var repositoryManager = new RepositoryManager(mockDbContext.Object);
        //var employeeRepository = repositoryManager.Employee;
        var companyId = Guid.NewGuid();
        var employeeParameters = new EmployeeParameters { MinAge = 10, MaxAge = 50, PageNumber = 1, PageSize = 10 };

        //Act
        var result = await _employeeRepository.GetEmployeesAsync(companyId, employeeParameters, false);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Count);
    }

    [Fact]
    public async void GetEmployee_ShouldReturnEmployee()
    {
        //Arrange
        //var mockDbContext = new Mock<RepositoryContext>(); 
        //var repositoryManager = new RepositoryManager(mockDbContext.Object);
        //var employeeRepository = repositoryManager.Employee;
        var companyId = Guid.NewGuid();
        var employeeId = Guid.NewGuid();

        //Act
        var result = await _employeeRepository.GetEmployeeAsync(companyId, employeeId, false);

        //Assert
        Assert.NotNull(result);
    }
}
