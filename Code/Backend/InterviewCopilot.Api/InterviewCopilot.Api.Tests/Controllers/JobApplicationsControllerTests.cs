using InterviewCopilot.Api.Controllers;
using InterviewCopilot.Api.Dtos;
using InterviewCopilot.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InterviewCopilot.Api.Tests.Controllers
{
    public class JobApplicationsControllerTests
    {
        private readonly Mock<IJobApplicationService> _mockService;
        private readonly JobApplicationsController _controller;

        public JobApplicationsControllerTests()
        {
            _mockService = new Mock<IJobApplicationService>();
            _controller = new JobApplicationsController(_mockService.Object);
        }

        [Fact]
        public async Task GetJobApplications_ShouldReturnOkResult_WithListOfJobApplications()
        {
            // Arrange
            var jobApplications = new List<JobApplicationDto>
            {
                new JobApplicationDto
                {
                    JobApplicationId = 1,
                    CompanyId = 1,
                    CompanyName = "Microsoft",
                    JobTitle = "Senior .NET Developer",
                    ApplicationStatus = "Applied",
                    ApplicationDate = DateTime.Today,
                    ExpectedSalary = 1500000,
                    Notes = "Prepare Web API"
                }
            };

            _mockService
                .Setup(service => service.GetAllAsync())
                .ReturnsAsync(jobApplications);

            // Act
            var result = await _controller.GetJobApplications();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedJobs = Assert.IsType<List<JobApplicationDto>>(okResult.Value);

            Assert.Single(returnedJobs);
            Assert.Equal("Senior .NET Developer", returnedJobs[0].JobTitle);
            Assert.Equal("Microsoft", returnedJobs[0].CompanyName);
        }

        [Fact]
        public async Task GetJobApplicationById_ShouldReturnOkResult_WhenJobApplicationExists()
        {
            // Arrange
            var jobApplication = new JobApplicationDto
            {
                JobApplicationId = 1,
                CompanyId = 1,
                CompanyName = "Infosys",
                JobTitle = "Backend Developer",
                ApplicationStatus = "Shortlisted",
                ApplicationDate = DateTime.Today
            };

            _mockService
                .Setup(service => service.GetByIdAsync(1))
                .ReturnsAsync(jobApplication);

            // Act
            var result = await _controller.GetJobApplicationById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedJob = Assert.IsType<JobApplicationDto>(okResult.Value);

            Assert.Equal(1, returnedJob.JobApplicationId);
            Assert.Equal("Backend Developer", returnedJob.JobTitle);
        }

        [Fact]
        public async Task GetJobApplicationById_ShouldReturnNotFound_WhenJobApplicationDoesNotExist()
        {
            // Arrange
            _mockService
                .Setup(service => service.GetByIdAsync(99))
                .ReturnsAsync((JobApplicationDto?)null);

            // Act
            var result = await _controller.GetJobApplicationById(99);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateJobApplication_ShouldReturnCreatedAtActionResult()
        {
            // Arrange
            var createDto = new CreateJobApplicationDto
            {
                CompanyId = 1,
                JobTitle = "Full Stack Developer",
                JobDescriptionUrl = "https://example.com/job",
                ApplicationDate = DateTime.Today,
                ApplicationStatus = "Applied",
                ExpectedSalary = 1200000,
                Notes = "React and .NET role"
            };

            var createdDto = new JobApplicationDto
            {
                JobApplicationId = 1,
                CompanyId = createDto.CompanyId,
                JobTitle = createDto.JobTitle,
                JobDescriptionUrl = createDto.JobDescriptionUrl,
                ApplicationDate = createDto.ApplicationDate,
                ApplicationStatus = createDto.ApplicationStatus,
                ExpectedSalary = createDto.ExpectedSalary,
                Notes = createDto.Notes
            };

            _mockService
                .Setup(service => service.CreateAsync(createDto))
                .ReturnsAsync(createdDto);

            // Act
            var result = await _controller.CreateJobApplication(createDto);

            // Assert
            var createdAtActionResult =
                Assert.IsType<CreatedAtActionResult>(result.Result);

            var returnedJob =
                Assert.IsType<JobApplicationDto>(createdAtActionResult.Value);

            Assert.Equal(nameof(_controller.GetJobApplicationById), createdAtActionResult.ActionName);
            Assert.Equal(1, returnedJob.JobApplicationId);
            Assert.Equal("Full Stack Developer", returnedJob.JobTitle);
        }

        [Fact]
        public async Task UpdateJobApplication_ShouldReturnNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var updateDto = new UpdateJobApplicationDto
            {
                CompanyId = 1,
                JobTitle = "Updated .NET Developer",
                ApplicationDate = DateTime.Today,
                ApplicationStatus = "Interview Scheduled",
                ExpectedSalary = 1600000,
                Notes = "Updated notes"
            };

            _mockService
                .Setup(service => service.UpdateAsync(1, updateDto))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateJobApplication(1, updateDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateJobApplication_ShouldReturnNotFound_WhenJobApplicationDoesNotExist()
        {
            // Arrange
            var updateDto = new UpdateJobApplicationDto
            {
                CompanyId = 1,
                JobTitle = "Updated .NET Developer",
                ApplicationDate = DateTime.Today,
                ApplicationStatus = "Interview Scheduled"
            };

            _mockService
                .Setup(service => service.UpdateAsync(99, updateDto))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateJobApplication(99, updateDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteJobApplication_ShouldReturnNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange
            _mockService
                .Setup(service => service.DeleteAsync(1))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteJobApplication(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteJobApplication_ShouldReturnNotFound_WhenJobApplicationDoesNotExist()
        {
            // Arrange
            _mockService
                .Setup(service => service.DeleteAsync(99))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteJobApplication(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}