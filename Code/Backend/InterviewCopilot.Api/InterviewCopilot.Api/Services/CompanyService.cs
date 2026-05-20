using InterviewCopilot.Api.DTOs;
using InterviewCopilot.Api.Models;
using InterviewCopilot.Api.Repositories;

namespace InterviewCopilot.Api.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<List<CompanyDto>> GetAllCompaniesAsync()
        {
            var companies = await _companyRepository.GetAllCompaniesAsync();

            return companies.Select(MapToDto).ToList();
        }

        public async Task<CompanyDto> AddCompanyAsync(
            CreateCompanyDto createCompanyDto)
        {
            var company = new Company
            {
                UserId = createCompanyDto.UserId,
                CompanyName = createCompanyDto.CompanyName,
                Website = createCompanyDto.Website,
                Industry = createCompanyDto.Industry,
                Location = createCompanyDto.Location,
                Notes = createCompanyDto.Notes,
                CreatedAt = DateTime.Now
            };

            var savedCompany =
                await _companyRepository.AddCompanyAsync(company);

            return MapToDto(savedCompany);
        }

        public async Task<CompanyDto?> UpdateCompanyAsync(
            int companyId,
            UpdateCompanyDto updateCompanyDto)
        {
            var existingCompany =
                await _companyRepository.GetCompanyByIdAsync(companyId);

            if (existingCompany == null)
            {
                return null;
            }

            existingCompany.CompanyName =
                updateCompanyDto.CompanyName;
            existingCompany.Website =
                updateCompanyDto.Website;
            existingCompany.Industry =
                updateCompanyDto.Industry;
            existingCompany.Location =
                updateCompanyDto.Location;
            existingCompany.Notes =
                updateCompanyDto.Notes;

            var updatedCompany =
                await _companyRepository.UpdateCompanyAsync(
                    existingCompany);

            if (updatedCompany == null)
            {
                return null;
            }

            return MapToDto(updatedCompany);
        }

        public async Task<bool> DeleteCompanyAsync(int companyId)
        {
            return await _companyRepository
                .DeleteCompanyAsync(companyId);
        }

        private CompanyDto MapToDto(Company company)
        {
            return new CompanyDto
            {
                CompanyId = company.CompanyId,
                UserId = company.UserId,
                CompanyName = company.CompanyName,
                Website = company.Website,
                Industry = company.Industry,
                Location = company.Location,
                Notes = company.Notes,
                CreatedAt = company.CreatedAt
            };
        }
    }
}