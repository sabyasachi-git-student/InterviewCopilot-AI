using InterviewCopilot.Api.Dtos;
using InterviewCopilot.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace InterviewCopilot.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobApplicationService _service;

        public JobApplicationsController(IJobApplicationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<JobApplicationDto>>> GetJobApplications()
        {
            var jobApplications = await _service.GetAllAsync();

            return Ok(jobApplications);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobApplicationDto>> GetJobApplicationById(int id)
        {
            var jobApplication = await _service.GetByIdAsync(id);

            if (jobApplication == null)
            {
                return NotFound();
            }

            return Ok(jobApplication);
        }

        [HttpPost]
        public async Task<ActionResult<JobApplicationDto>> CreateJobApplication(
            CreateJobApplicationDto dto)
        {
            var createdJobApplication = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetJobApplicationById),
                new { id = createdJobApplication.JobApplicationId },
                createdJobApplication);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobApplication(
            int id,
            UpdateJobApplicationDto dto)
        {
            var isUpdated = await _service.UpdateAsync(id, dto);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobApplication(int id)
        {
            var isDeleted = await _service.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}