using InterviewCopilot.Api.Dtos;
using InterviewCopilot.Api.Models;
using InterviewCopilot.Api.Repositories;
using InterviewCopilot.Api.Services;
using InterviewCopilotAPI.Models;
using Moq;

namespace InterviewCopilot.Api.Tests.Services
{
    public class JobApplicationServiceTests
    {
        private readonly Mock<IJobApplicationRepository> _mockRepository;
        private readonly JobApplicationService _service;

        public JobApplicationServiceTests()
        {
            _mockRepository = new Mock<IJobApplicationRepository>();
            _service = new JobApplicationService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfJobApplications()
        {
            // Arrange
            var jobApplications = new List<JobApplication>
            {
                new JobApplication
                {
                    JobApplicationId = 1,
                    CompanyId = 1,
                    JobTitle = "Senior .NET Developer",
                    ApplicationStatus = "Applied",
                    ApplicationDate = DateTime.Today,
                    ExpectedSalary = 1500000,
                    Notes = "Prepare Web API and SQL",
                    Company = new Company
                    {
                        CompanyId = 1,
                        CompanyName = "Microsoft"
                    }
                }
            };

            _mockRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(jobApplications);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Senior .NET Developer", result[0].JobTitle);
            Assert.Equal("Microsoft", result[0].CompanyName);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnJobApplication_WhenJobApplicationExists()
        {
            // Arrange
            var jobApplication = new JobApplication
            {
                JobApplicationId = 1,
                CompanyId = 1,
                JobTitle = "Backend Developer",
                ApplicationStatus = "Shortlisted",
                ApplicationDate = DateTime.Today,
                Company = new Company
                {
                    CompanyId = 1,
                    CompanyName = "Infosys"
                }
            };

            _mockRepository
                .Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(jobApplication);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.JobApplicationId);
            Assert.Equal("Backend Developer", result.JobTitle);
            Assert.Equal("Infosys", result.CompanyName);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenJobApplicationDoesNotExist()
        {
            // Arrange
            _mockRepository
                .Setup(repo => repo.GetByIdAsync(99))
                .ReturnsAsync((JobApplication?)null);

            // Act
            var result = await _service.GetByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateAndReturnJobApplication()
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

            var createdJobApplication = new JobApplication
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

            _mockRepository
                .Setup(repo => repo.CreateAsync(It.IsAny<JobApplication>()))
                .ReturnsAsync(createdJobApplication);

            // Act
            var result = await _service.CreateAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.JobApplicationId);
            Assert.Equal("Full Stack Developer", result.JobTitle);

            _mockRepository.Verify(
                repo => repo.CreateAsync(It.IsAny<JobApplication>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnTrue_WhenUpdateIsSuccessful()
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

            _mockRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<JobApplication>()))
                .ReturnsAsync(true);

            // Act
            var result = await _service.UpdateAsync(1, updateDto);

            // Assert
            Assert.True(result);

            _mockRepository.Verify(
                repo => repo.UpdateAsync(It.IsAny<JobApplication>()),
                Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenDeleteIsSuccessful()
        {
            // Arrange
            _mockRepository
                .Setup(repo => repo.DeleteAsync(1))
                .ReturnsAsync(true);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.True(result);

            _mockRepository.Verify(
                repo => repo.DeleteAsync(1),
                Times.Once);
        }
    }
}