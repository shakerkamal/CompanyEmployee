using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;

namespace CompanyEmployee.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public EmployeesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public IActionResult GetEmployeesForCompany(Guid companyId)
        {
            var company = _serviceManager.EmployeeService.GetEmployees(companyId, trackChanges: false);

            return Ok(company);
        }

        [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
        public IActionResult GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var employee = _serviceManager.EmployeeService.GetEmployee(companyId, id, trackChanges: false);
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeCreationDto employee)
        {
            if (employee == null)
            {
                return BadRequest("CompanyCreationDto object is null");
            }

            var employeeToReturn = _serviceManager.EmployeeService.CreateEmployee(companyId, employee, trackChanges:false);

            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeToReturn.Id }, employeeToReturn);
        }

        [HttpDelete("{id:guid}")] 
        public IActionResult DeleteEmployeeForCompany(Guid companyId, Guid id) 
        { 
            _serviceManager.EmployeeService.DeleteEmployeeForCompany(companyId, id, trackChanges: false);
            return NoContent(); 
        }

        [HttpPut("{id:guid}")] 
        public IActionResult UpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] EmployeeUpdateDto employee) 
        { 
            if (employee is null) 
                return BadRequest("EmployeeForUpdateDto object is null");
            _serviceManager.EmployeeService.UpdateEmployeeForCompany(companyId, id, employee, compTrackChanges: false, trackChanges: true); 
            return NoContent(); 
        }
    }
}