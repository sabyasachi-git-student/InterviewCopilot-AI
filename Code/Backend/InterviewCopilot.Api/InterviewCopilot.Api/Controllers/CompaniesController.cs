using InterviewCopilot.Api.DTOs;
using InterviewCopilot.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterviewCopilot.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CompanyDto>>> GetCompanies()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return Ok(companies);
        }

        [HttpPost]
        public async Task<ActionResult<CompanyDto>> AddCompany(CreateCompanyDto createCompanyDto)
        {
            var company = await _companyService.AddCompanyAsync(createCompanyDto);
            return Ok(company);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CompanyDto>> UpdateCompany(
            int id,
            UpdateCompanyDto updateCompanyDto)
        {
            var updatedCompany = await _companyService.UpdateCompanyAsync(id, updateCompanyDto);

            if (updatedCompany == null)
            {
                return NotFound();
            }

            return Ok(updatedCompany);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCompany(int id)
        {
            try
            {
                var isDeleted = await _companyService.DeleteCompanyAsync(id);

                if (!isDeleted)
                {
                    return NotFound("Company not found.");
                }

                return NoContent();
            }
            catch (DbUpdateException)
            {
                return BadRequest("This company cannot be deleted because job applications are linked to it.");
            }
        }
    }
}