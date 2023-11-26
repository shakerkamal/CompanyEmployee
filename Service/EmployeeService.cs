using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<EmployeeDto> employees, MetaData metaData)> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
        {
            if (!employeeParameters.ValidAgeRange)
                throw new MaxAgeRangeException();
            await CheckIfCompanyExists(companyId, trackChanges);
            var employeesWithMetaData = await _repositoryManager.Employee
                .GetEmployeesAsync(companyId, employeeParameters, trackChanges);

            var result = _mapper.Map<IEnumerable<EmployeeDto>>(employeesWithMetaData);
            return (employees: result, metaData: employeesWithMetaData.MetaData);
        }

        public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
        {
            await CheckIfCompanyExists(companyId, trackChanges);
            var employeeForCompany = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, trackChanges);
            var result = _mapper.Map<EmployeeDto>(employeeForCompany);
            return result;
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(Guid companyId, EmployeeCreationDto employeeCreationDto, bool trackChanges)
        {
            await CheckIfCompanyExists(companyId, trackChanges);

            var employeeEntity = _mapper.Map<Employee>(employeeCreationDto);

            await _repositoryManager.Employee.CreateEmployeeAsync(companyId, employeeEntity);
            await _repositoryManager.SaveAsync();

            var result = _mapper.Map<EmployeeDto>(employeeEntity);
            return result;
        }

        public async Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid id, bool trackChanges)
        {
            await CheckIfCompanyExists(companyId, trackChanges);
            var employeeForCompany = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, trackChanges);

            _repositoryManager.Employee.DeleteEmployee(employeeForCompany);
            await _repositoryManager.SaveAsync();
        }

        public async Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, EmployeeUpdateDto employeeUpdateDto, bool compTrackChanges, bool trackChanges)
        {
            await CheckIfCompanyExists(companyId, trackChanges);
            var employeeForCompany = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, trackChanges);

            _mapper.Map(employeeUpdateDto, employeeForCompany);
            await _repositoryManager.SaveAsync();
        }

        private async Task CheckIfCompanyExists(Guid id, bool trackChanges)
        {
            var company = await _repositoryManager.Company.GetCompanyAsync(id, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(id);
        }

        private async Task<Employee> GetEmployeeForCompanyAndCheckIfItExists(Guid companyId, Guid id, bool trackChanges) 
        { 
            var employeeDb = await _repositoryManager.Employee.GetEmployeeAsync(companyId, id, trackChanges);
            if (employeeDb is null)
                throw new EmployeeNotFoundException(id); 
            return employeeDb; 
        }
    }
}
