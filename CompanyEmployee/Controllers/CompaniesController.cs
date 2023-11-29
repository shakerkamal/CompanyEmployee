﻿using CompanyEmployee.ActionFilters;
using CompanyEmployee.ModelBinders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyEmployee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CompaniesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet(Name ="GetCompanies")]
        [Authorize]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _serviceManager.CompanyService.GetAllCompaniesAsync(trackChanges: false);
            return Ok(companies);
        }

        [HttpGet("{id}", Name = "CompanyById")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var company = await _serviceManager.CompanyService.GetCompanyAsync(id, trackChanges: false);
            return Ok(company);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> PostCompany([FromBody] CompanyCreationDto company)
        {
            var createdCompany = await _serviceManager.CompanyService.CreateCompanyAsync(company);
            return CreatedAtRoute("CompanyById", new { Id = createdCompany.Id }, createdCompany);
        }

        [HttpGet("collection/({ids})", Name = "CompanyCollection")] 
        public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType =typeof(ArrayModelBinder))]IEnumerable<Guid> ids) 
        { 
            var companies = await _serviceManager.CompanyService.GetByIdsAsync(ids, trackChanges: false);
            return Ok(companies); 
        }

        [HttpPost("collection")] 
        public async Task<IActionResult> CreateCompanyCollection([FromBody] IEnumerable<CompanyCreationDto> companyCollection) 
        { 
            var result = await _serviceManager.CompanyService.CreateCompanyCollectionAsync(companyCollection);

            return CreatedAtRoute("CompanyCollection", new { result.ids }, result.companies); 
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            await _serviceManager.CompanyService.DeleteCompanyAsync(id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyUpdateDto company)
        {
            await _serviceManager.CompanyService.UpdateCompanyAsync(id, company, trackChanges: true);
            return NoContent();
        }
    }
}
