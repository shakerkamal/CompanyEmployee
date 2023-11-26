using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.ComponentModel.Design;

namespace Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public CompanyService(IRepositoryManager repositoryManager, ILoggerManager loggerManager,IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool trackChanges)
        {
            var companies = await _repositoryManager.Company.GetAllCompaniesAsync(trackChanges);
            var result = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return result;
        }

        public async Task<CompanyDto> GetCompanyAsync(Guid companyId, bool trackChanges)
        {
            var company = await _repositoryManager.Company.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            var result = _mapper.Map<CompanyDto>(company);
            return result;
        }

        public async Task<CompanyDto> CreateCompanyAsync(CompanyCreationDto company)
        {
            var companyEntity = _mapper.Map<Company>(company);

            await _repositoryManager.Company.CreateCompanyAsync(companyEntity);
            await _repositoryManager.SaveAsync();

            var result = _mapper.Map<CompanyDto>(companyEntity);
            return result;
        }

        public async Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null) 
                throw new IdParametersBadRequestException();
            
            var companyEntities = await _repositoryManager.Company.GetByIdsAsync(ids, trackChanges); 
            if (ids.Count() != companyEntities.Count()) 
                throw new CollectionByIdsBadRequestException(); 

            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities); 
            return companiesToReturn;
        }

        public async Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollectionAsync(IEnumerable<CompanyCreationDto> companyCollection)
        {
            if (companyCollection is null) 
                throw new CompanyCollectionBadRequest(); 
            
            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection); 
            
            foreach (var company in companyEntities) 
            { 
                await _repositoryManager.Company.CreateCompanyAsync(company); 
            }
            await _repositoryManager.SaveAsync(); 
            
            var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities); 
            var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));

            return (companies: companyCollectionToReturn, ids: ids);
        }

        public async Task DeleteCompanyAsync(Guid companyId, bool trackChanges)
        {
            var company = await _repositoryManager.Company.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            _repositoryManager.Company.DeleteCompany(company);
            await _repositoryManager.SaveAsync();
        }

        public async Task UpdateCompanyAsync(Guid companyId, CompanyUpdateDto companyUpdateDto, bool trackChanges)
        {
            var company = await _repositoryManager.Company.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            _mapper.Map(companyUpdateDto, company);
            await _repositoryManager.SaveAsync();
        }
    }
}
