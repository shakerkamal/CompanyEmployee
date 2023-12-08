using Moq;
using Repository;

namespace CompanyEmployee.Test;

public class RepositoryManagerTests
{
    [Fact]
    public void Company_ShouldReturnCompanyRepository()
    {
        //Arrange
        var mockDbContext = new Mock<RepositoryContext>();
        var repositoryManager = new RepositoryManager(mockDbContext.Object);

        //Act
        var companyRepository = repositoryManager.Company;

        //Assert
        Assert.NotNull(companyRepository);
    }

    [Fact]
    public void Employee_ShouldReturnEmployeeRepository()
    {
        //Arrange
        var mockDbContext = new Mock<RepositoryContext>();
        var repositoryManager = new RepositoryManager(mockDbContext.Object);

        //Act
        var employeeRepository = repositoryManager.Employee;

        //Assert
        Assert.NotNull(employeeRepository);
    }

    [Fact]
    public async void DisposeAsync_ShouldDisposeContext()
    {
        //Arrange
        var mockDbContext = new Mock<RepositoryContext>();
        var repositoryManager = new RepositoryManager(mockDbContext.Object);

        //Act
        await repositoryManager.DisposeAsync();

        //Asset
        mockDbContext.Verify(c => c.DisposeAsync(), Times.Once);
    }
}
