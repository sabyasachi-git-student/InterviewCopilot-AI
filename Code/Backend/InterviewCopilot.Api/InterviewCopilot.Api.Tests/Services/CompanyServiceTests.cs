using InterviewCopilot.Api.DTOs;
using InterviewCopilot.Api.Models;
using InterviewCopilot.Api.Repositories;
using InterviewCopilot.Api.Services;
using Moq;

namespace InterviewCopilot.Api.Tests.Services
{
    public class CompanyServiceTests
    {
        [Fact]
        public async Task AddCompanyAsync_ShouldReturnCompanyDto_WhenCompanyIsAdded()
        {
            // Arrange
            var mockRepository = new Mock<ICompanyRepository>();

            var createCompanyDto = new CreateCompanyDto
            {
                UserId = 1,
                CompanyName = "Microsoft",
                Website = "https://www.microsoft.com",
                Industry = "Technology",
                Location = "Hyderabad",
                Notes = "Good .NET opportunities"
            };

            var savedCompany = new Company
            {
                CompanyId = 1,
                UserId = 1,
                CompanyName = "Microsoft",
                Website = "https://www.microsoft.com",
                Industry = "Technology",
                Location = "Hyderabad",
                Notes = "Good .NET opportunities",
                CreatedAt = DateTime.Now
            };

            mockRepository
                .Setup(repo => repo.AddCompanyAsync(It.IsAny<Company>()))
                .ReturnsAsync(savedCompany);

            var service = new CompanyService(mockRepository.Object);

            // Act
            var result = await service.AddCompanyAsync(createCompanyDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CompanyId);
            Assert.Equal("Microsoft", result.CompanyName);
            Assert.Equal("Technology", result.Industry);

            mockRepository.Verify(
                repo => repo.AddCompanyAsync(It.IsAny<Company>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateCompanyAsync_ShouldReturnUpdatedCompanyDto_WhenCompanyExists()
        {
            // Arrange
            var mockRepository = new Mock<ICompanyRepository>();

            var existingCompany = new Company
            {
                CompanyId = 1,
                UserId = 1,
                CompanyName = "Microsoft",
                Website = "https://www.microsoft.com",
                Industry = "Technology",
                Location = "Hyderabad",
                Notes = "Old notes",
                CreatedAt = DateTime.Now
            };

            var updateDto = new UpdateCompanyDto
            {
                CompanyName = "Microsoft India",
                Website = "https://www.microsoft.com/en-in",
                Industry = "Technology",
                Location = "Bangalore",
                Notes = "Updated notes"
            };

            mockRepository
                .Setup(repo => repo.GetCompanyByIdAsync(1))
                .ReturnsAsync(existingCompany);

            mockRepository
                .Setup(repo => repo.UpdateCompanyAsync(It.IsAny<Company>()))
                .ReturnsAsync((Company company) => company);

            var service = new CompanyService(mockRepository.Object);

            // Act
            var result = await service.UpdateCompanyAsync(1, updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Microsoft India", result.CompanyName);
            Assert.Equal("Bangalore", result.Location);
            Assert.Equal("Updated notes", result.Notes);

            mockRepository.Verify(
                repo => repo.GetCompanyByIdAsync(1),
                Times.Once);

            mockRepository.Verify(
                repo => repo.UpdateCompanyAsync(It.IsAny<Company>()),
                Times.Once);
        }

        [Fact]
        public async Task DeleteCompanyAsync_ShouldReturnTrue_WhenCompanyIsDeleted()
        {
            // Arrange
            var mockRepository = new Mock<ICompanyRepository>();

            mockRepository
                .Setup(repo => repo.DeleteCompanyAsync(1))
                .ReturnsAsync(true);

            var service = new CompanyService(mockRepository.Object);

            // Act
            var result = await service.DeleteCompanyAsync(1);

            // Assert
            Assert.True(result);

            mockRepository.Verify(
                repo => repo.DeleteCompanyAsync(1),
                Times.Once);
        }
    }
}