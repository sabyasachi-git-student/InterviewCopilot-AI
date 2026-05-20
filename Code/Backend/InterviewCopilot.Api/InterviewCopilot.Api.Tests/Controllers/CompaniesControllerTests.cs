using InterviewCopilot.Api.Controllers;
using InterviewCopilot.Api.DTOs;
using InterviewCopilot.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InterviewCopilot.Api.Tests.Controllers
{
    public class CompaniesControllerTests
    {
        [Fact]
        public async Task GetCompanies_ShouldReturnOkResult_WithCompanyList()
        {
            // Arrange
            var mockService = new Mock<ICompanyService>();

            var companies = new List<CompanyDto>
            {
                new CompanyDto
                {
                    CompanyId = 1,
                    UserId = 1,
                    CompanyName = "Microsoft",
                    Industry = "Technology",
                    Location = "Hyderabad"
                }
            };

            mockService
                .Setup(service => service.GetAllCompaniesAsync())
                .ReturnsAsync(companies);

            var controller = new CompaniesController(mockService.Object);

            // Act
            var result = await controller.GetCompanies();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCompanies = Assert.IsType<List<CompanyDto>>(okResult.Value);

            Assert.Single(returnedCompanies);
            Assert.Equal("Microsoft", returnedCompanies[0].CompanyName);
        }

        [Fact]
        public async Task AddCompany_ShouldReturnOkResult_WithCreatedCompany()
        {
            // Arrange
            var mockService = new Mock<ICompanyService>();

            var createDto = new CreateCompanyDto
            {
                UserId = 1,
                CompanyName = "Google",
                Industry = "Technology",
                Location = "Bangalore"
            };

            var createdCompany = new CompanyDto
            {
                CompanyId = 2,
                UserId = 1,
                CompanyName = "Google",
                Industry = "Technology",
                Location = "Bangalore"
            };

            mockService
                .Setup(service => service.AddCompanyAsync(createDto))
                .ReturnsAsync(createdCompany);

            var controller = new CompaniesController(mockService.Object);

            // Act
            var result = await controller.AddCompany(createDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCompany = Assert.IsType<CompanyDto>(okResult.Value);

            Assert.Equal(2, returnedCompany.CompanyId);
            Assert.Equal("Google", returnedCompany.CompanyName);
        }

        [Fact]
        public async Task UpdateCompany_ShouldReturnOkResult_WhenCompanyExists()
        {
            // Arrange
            var mockService = new Mock<ICompanyService>();

            var updateDto = new UpdateCompanyDto
            {
                CompanyName = "Microsoft India",
                Industry = "Technology",
                Location = "Bangalore",
                Website = "https://www.microsoft.com",
                Notes = "Updated company"
            };

            var updatedCompany = new CompanyDto
            {
                CompanyId = 1,
                UserId = 1,
                CompanyName = "Microsoft India",
                Industry = "Technology",
                Location = "Bangalore",
                Website = "https://www.microsoft.com",
                Notes = "Updated company"
            };

            mockService
                .Setup(service => service.UpdateCompanyAsync(1, updateDto))
                .ReturnsAsync(updatedCompany);

            var controller = new CompaniesController(mockService.Object);

            // Act
            var result = await controller.UpdateCompany(1, updateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCompany = Assert.IsType<CompanyDto>(okResult.Value);

            Assert.Equal("Microsoft India", returnedCompany.CompanyName);
            Assert.Equal("Bangalore", returnedCompany.Location);
        }

        [Fact]
        public async Task UpdateCompany_ShouldReturnNotFound_WhenCompanyDoesNotExist()
        {
            // Arrange
            var mockService = new Mock<ICompanyService>();

            var updateDto = new UpdateCompanyDto
            {
                CompanyName = "Unknown Company"
            };

            mockService
                .Setup(service => service.UpdateCompanyAsync(99, updateDto))
                .ReturnsAsync((CompanyDto?)null);

            var controller = new CompaniesController(mockService.Object);

            // Act
            var result = await controller.UpdateCompany(99, updateDto);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task DeleteCompany_ShouldReturnNoContent_WhenCompanyIsDeleted()
        {
            // Arrange
            var mockService = new Mock<ICompanyService>();

            mockService
                .Setup(service => service.DeleteCompanyAsync(1))
                .ReturnsAsync(true);

            var controller = new CompaniesController(mockService.Object);

            // Act
            var result = await controller.DeleteCompany(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCompany_ShouldReturnNotFound_WhenCompanyDoesNotExist()
        {
            // Arrange
            var mockService = new Mock<ICompanyService>();

            mockService
                .Setup(service => service.DeleteCompanyAsync(99))
                .ReturnsAsync(false);

            var controller = new CompaniesController(mockService.Object);

            // Act
            var result = await controller.DeleteCompany(99);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Company not found.", notFoundResult.Value);
        }
    }
}