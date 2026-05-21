using InterviewCopilot.Api.Models;
using InterviewCopilotAPI.Models;

namespace InterviewCopilot.Api.Repositories
{
    public interface IJobApplicationRepository
    {
        Task<List<JobApplication>> GetAllAsync();

        Task<JobApplication?> GetByIdAsync(int id);

        Task<JobApplication> CreateAsync(JobApplication jobApplication);

        Task<bool> UpdateAsync(JobApplication jobApplication);

        Task<bool> DeleteAsync(int id);
    }
}