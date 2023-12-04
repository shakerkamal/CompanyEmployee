using Application.Commands.CreateCompanies;
using Application.Commands.CreateCompany;
using Application.Commands.DeleteCompany;
using Application.Commands.UpdateCompany;
using Application.Queries.GetCompanies;
using Application.Queries.GetCompaniesByIds;
using Application.Queries.GetCompany;
using CompanyEmployee.ActionFilters;
using CompanyEmployee.ModelBinders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyEmployee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompaniesController : ControllerBase
    {
        private readonly ISender _sender;

        public CompaniesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet(Name ="GetCompanies")]
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _sender.Send(new GetCompaniesQuery(TrackChanges: false));
            return Ok(companies);
        }

        [HttpGet("{id}", Name = "CompanyById")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var company = await _sender.Send(new GetCompanyQuery(id, false));
            return Ok(company);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> PostCompany([FromBody] CompanyCreationDto company)
        {
            var createdCompany = await _sender.Send(new CreateCompanyCommand(company));
            return CreatedAtRoute("CompanyById", new { Id = createdCompany.Id }, createdCompany);
        }

        [HttpGet("collection/({ids})", Name = "CompanyCollection")] 
        public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType =typeof(ArrayModelBinder))]IEnumerable<Guid> ids) 
        { 
            var companies = await _sender.Send(new GetCompaniesByIdsQuery(ids, TrackChanges: false));
            return Ok(companies); 
        }

        [HttpPost("collection")] 
        public async Task<IActionResult> CreateCompanyCollection([FromBody] IEnumerable<CompanyCreationDto> companyCollection) 
        { 
            var result = await _sender.Send(new CreateCompaniesCommand(companyCollection));

            return CreatedAtRoute("CompanyCollection", new { result.ids }, result.companies); 
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            await _sender.Send(new DeleteCompanyCommand(id,TrackChanges: false));
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyUpdateDto company)
        {
            await _sender.Send(new UpdateCompanyCommand(id, company, TrackChanges: true));
            return NoContent();
        }
    }
}
