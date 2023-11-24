using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CompanyEmployee.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetEmployeesForCompany(Guid companyId)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);

            if (company == null)
            {
                return NotFound();
            }

            var employees = _repository.Employee.GetEmployees(companyId, trackChanges: false);
            var mappedEmployees = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(mappedEmployees);
        }

        [HttpGet("{id}", Name = "GetEmployeeForCompany")]
        public IActionResult GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);

            if (company == null)
            {
                return NotFound();
            }

            var employee = _repository.Employee.GetEmployee(companyId, id, trackChanges: false);
            if (employee == null)
            {
                return NotFound();
            }
            var mappedEmployee = _mapper.Map<EmployeeDto>(employee);
            return Ok(mappedEmployee);
        }

        [HttpPost]
        public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeCreationDto employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);

            if (company == null)
            {
                return NotFound();
            }

            var employeeEntity = _mapper.Map<Employee>(employee);

            _repository.Employee.CreateEmployee(companyId, employeeEntity);
            _repository.Save();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeToReturn.Id }, employeeToReturn);
        }
    }
}
