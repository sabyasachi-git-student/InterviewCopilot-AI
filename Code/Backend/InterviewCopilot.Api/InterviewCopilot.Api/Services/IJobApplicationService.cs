using InterviewCopilot.Api.Dtos;

namespace InterviewCopilot.Api.Services
{
    public interface IJobApplicationService
    {
        Task<List<JobApplicationDto>> GetAllAsync();

        Task<JobApplicationDto?> GetByIdAsync(int id);

        Task<JobApplicationDto> CreateAsync(CreateJobApplicationDto dto);

        Task<bool> UpdateAsync(int id, UpdateJobApplicationDto dto);

        Task<bool> DeleteAsync(int id);
    }
}