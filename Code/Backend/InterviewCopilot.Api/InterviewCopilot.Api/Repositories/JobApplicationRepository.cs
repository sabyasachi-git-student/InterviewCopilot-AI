using InterviewCopilot.Api.Data;
using InterviewCopilot.Api.Models;
using InterviewCopilotAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InterviewCopilot.Api.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public JobApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<JobApplication>> GetAllAsync()
        {
            return await _context.JobApplications
                .Include(j => j.Company)
                .ToListAsync();
        }

        public async Task<JobApplication?> GetByIdAsync(int id)
        {
            return await _context.JobApplications
                .Include(j => j.Company)
                .FirstOrDefaultAsync(j => j.JobApplicationId == id);
        }

        public async Task<JobApplication> CreateAsync(JobApplication jobApplication)
        {
            jobApplication.CreatedAt = DateTime.Now;

            _context.JobApplications.Add(jobApplication);
            await _context.SaveChangesAsync();

            return jobApplication;
        }

        public async Task<bool> UpdateAsync(JobApplication jobApplication)
        {
            var existingJobApplication = await _context.JobApplications
                .FirstOrDefaultAsync(j => j.JobApplicationId == jobApplication.JobApplicationId);

            if (existingJobApplication == null)
            {
                return false;
            }

            existingJobApplication.CompanyId = jobApplication.CompanyId;
            existingJobApplication.JobTitle = jobApplication.JobTitle;
            existingJobApplication.JobDescriptionUrl = jobApplication.JobDescriptionUrl;
            existingJobApplication.ApplicationDate = jobApplication.ApplicationDate;
            existingJobApplication.ApplicationStatus = jobApplication.ApplicationStatus;
            existingJobApplication.ExpectedSalary = jobApplication.ExpectedSalary;
            existingJobApplication.Notes = jobApplication.Notes;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var jobApplication = await _context.JobApplications
                .FirstOrDefaultAsync(j => j.JobApplicationId == id);

            if (jobApplication == null)
            {
                return false;
            }

            _context.JobApplications.Remove(jobApplication);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}