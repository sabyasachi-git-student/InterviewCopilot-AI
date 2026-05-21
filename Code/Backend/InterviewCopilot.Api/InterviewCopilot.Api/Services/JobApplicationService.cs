using InterviewCopilot.Api.Dtos;
using InterviewCopilot.Api.Models;
using InterviewCopilot.Api.Repositories;
using InterviewCopilotAPI.Models;

namespace InterviewCopilot.Api.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _repository;

        public JobApplicationService(IJobApplicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<JobApplicationDto>> GetAllAsync()
        {
            var jobApplications = await _repository.GetAllAsync();

            return jobApplications.Select(j => new JobApplicationDto
            {
                JobApplicationId = j.JobApplicationId,
                CompanyId = j.CompanyId,
                CompanyName = j.Company?.CompanyName,
                JobTitle = j.JobTitle,
                JobDescriptionUrl = j.JobDescriptionUrl,
                ApplicationDate = j.ApplicationDate,
                ApplicationStatus = j.ApplicationStatus,
                ExpectedSalary = j.ExpectedSalary,
                Notes = j.Notes
            }).ToList();
        }

        public async Task<JobApplicationDto?> GetByIdAsync(int id)
        {
            var j = await _repository.GetByIdAsync(id);

            if (j == null)
            {
                return null;
            }

            return new JobApplicationDto
            {
                JobApplicationId = j.JobApplicationId,
                CompanyId = j.CompanyId,
                CompanyName = j.Company?.CompanyName,
                JobTitle = j.JobTitle,
                JobDescriptionUrl = j.JobDescriptionUrl,
                ApplicationDate = j.ApplicationDate,
                ApplicationStatus = j.ApplicationStatus,
                ExpectedSalary = j.ExpectedSalary,
                Notes = j.Notes
            };
        }

        public async Task<JobApplicationDto> CreateAsync(CreateJobApplicationDto dto)
        {
            var jobApplication = new JobApplication
            {
                CompanyId = dto.CompanyId,
                JobTitle = dto.JobTitle,
                JobDescriptionUrl = dto.JobDescriptionUrl,
                ApplicationDate = dto.ApplicationDate,
                ApplicationStatus = dto.ApplicationStatus,
                ExpectedSalary = dto.ExpectedSalary,
                Notes = dto.Notes,
                CreatedAt = DateTime.Now
            };

            var createdJobApplication = await _repository.CreateAsync(jobApplication);

            return new JobApplicationDto
            {
                JobApplicationId = createdJobApplication.JobApplicationId,
                CompanyId = createdJobApplication.CompanyId,
                JobTitle = createdJobApplication.JobTitle,
                JobDescriptionUrl = createdJobApplication.JobDescriptionUrl,
                ApplicationDate = createdJobApplication.ApplicationDate,
                ApplicationStatus = createdJobApplication.ApplicationStatus,
                ExpectedSalary = createdJobApplication.ExpectedSalary,
                Notes = createdJobApplication.Notes
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateJobApplicationDto dto)
        {
            var jobApplication = new JobApplication
            {
                JobApplicationId = id,
                CompanyId = dto.CompanyId,
                JobTitle = dto.JobTitle,
                JobDescriptionUrl = dto.JobDescriptionUrl,
                ApplicationDate = dto.ApplicationDate,
                ApplicationStatus = dto.ApplicationStatus,
                ExpectedSalary = dto.ExpectedSalary,
                Notes = dto.Notes
            };

            return await _repository.UpdateAsync(jobApplication);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}