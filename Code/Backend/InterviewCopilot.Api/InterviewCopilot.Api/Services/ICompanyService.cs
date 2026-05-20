using InterviewCopilot.Api.DTOs;

namespace InterviewCopilot.Api.Services
{
    public interface ICompanyService
    {
        Task<List<CompanyDto>> GetAllCompaniesAsync();

        Task<CompanyDto> AddCompanyAsync(CreateCompanyDto createCompanyDto);

        Task<CompanyDto?> UpdateCompanyAsync(
            int companyId,
            UpdateCompanyDto updateCompanyDto);

        Task<bool> DeleteCompanyAsync(int companyId);
    }
}