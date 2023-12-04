using Application.Commands.CreateEmployee;
using Application.Commands.DeleteEmployee;
using Application.Commands.UpdateEmployee;
using Application.Queries.GetEmployee;
using Application.Queries.GetEmployees;
using CompanyEmployee.ActionFilters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace CompanyEmployee.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ISender _sender;

        public EmployeesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeesForCompany(Guid companyId, [FromQuery] EmployeeParameters employeeParameters)
        {
            var pagedResponse = await _sender.Send(new GetEmployeesQuery(companyId, employeeParameters, TrackChanges: false));

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResponse.metaData));

            return Ok(pagedResponse.employees);
        }

        [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
        public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var employee = await _sender.Send(new GetEmployeeQuery(companyId, id, TrackChanges: false));
            return Ok(employee);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeCreationDto employee)
        {
            var employeeToReturn = await _sender.Send(new CreateEmployeeCommand(companyId, employee, TrackChanges: false));
            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeToReturn.Id }, employeeToReturn);
        }

        [HttpDelete("{id:guid}")] 
        public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid id) 
        { 
            await _sender.Send(new DeleteEmployeeCommand(companyId, id, TrackChanges: false));
            return NoContent(); 
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] EmployeeUpdateDto employee) 
        { 
            await _sender.Send(new UpdateEmployeeCommand(companyId, id, employee, CompTrackChanges: false, TrackChanges: true)); 
            return NoContent(); 
        }
    }
}