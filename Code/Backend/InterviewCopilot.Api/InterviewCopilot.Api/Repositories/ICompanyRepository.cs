using InterviewCopilot.Api.Models;

namespace InterviewCopilot.Api.Repositories
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAllCompaniesAsync();
        Task<Company?> GetCompanyByIdAsync(int companyId);
        Task<Company> AddCompanyAsync(Company company);
        Task<Company?> UpdateCompanyAsync(Company company);
        Task<bool> DeleteCompanyAsync(int companyId);
    }
}