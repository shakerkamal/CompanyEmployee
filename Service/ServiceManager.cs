using AutoMapper;
using Contracts;
using Entities.ConfigurationModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IAutheticationService> _authenticationService; 

        public ServiceManager(IRepositoryManager repositoryManager, 
                            ILoggerManager loggerManager,
                            IMapper mapper,
                            UserManager<User> userManager,
                            IOptions<JwtConfiguration> configuration)
        {
            _companyService = new Lazy<ICompanyService>(() => new CompanyService(repositoryManager, loggerManager, mapper));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repositoryManager, loggerManager, mapper));
            _authenticationService = new Lazy<IAutheticationService>(() => new AuthenticationService(loggerManager, mapper, userManager, configuration));
        }
        public ICompanyService CompanyService => _companyService.Value;

        public IEmployeeService EmployeeService => _employeeService.Value;

        public IAutheticationService AutheticationService => _authenticationService.Value;
    }
}
