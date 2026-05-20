using InterviewCopilot.Api.Data;
using InterviewCopilot.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InterviewCopilot.Api.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Company>> GetAllCompaniesAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company?> GetCompanyByIdAsync(int companyId)
        {
            return await _context.Companies
                .FirstOrDefaultAsync(c => c.CompanyId == companyId);
        }

        public async Task<Company> AddCompanyAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return company;
        }

        public async Task<Company?> UpdateCompanyAsync(Company company)
        {
            var existingCompany = await _context.Companies
                .FirstOrDefaultAsync(c => c.CompanyId == company.CompanyId);

            if (existingCompany == null)
            {
                return null;
            }

            existingCompany.CompanyName = company.CompanyName;
            existingCompany.Website = company.Website;
            existingCompany.Industry = company.Industry;
            existingCompany.Location = company.Location;
            existingCompany.Notes = company.Notes;

            await _context.SaveChangesAsync();

            return existingCompany;
        }

        public async Task<bool> DeleteCompanyAsync(int companyId)
        {
            var company = await _context.Companies
                .FirstOrDefaultAsync(c => c.CompanyId == companyId);

            if (company == null)
            {
                return false;
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}