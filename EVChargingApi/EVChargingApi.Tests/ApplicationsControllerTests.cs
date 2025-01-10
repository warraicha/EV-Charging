using Xunit;
using Moq;
using EVChargingAPI.Controllers;
using EVChargingAPI.Services;
using EVChargingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EVChargingApi.Tests;
public class ApplicationsControllerTests
{
    private readonly Mock<IApplicationService> _mockService;
    private readonly ApplicationsController _controller;

    public ApplicationsControllerTests()
    {
        _mockService = new Mock<IApplicationService>();
        _controller = new ApplicationsController(_mockService.Object);
    }

    [Fact]
    public async Task GetApplications_ReturnsAllApplications()
    {
        // Arrange
        var applications = new List<Application>
        {
            new Application { Id = Guid.NewGuid(), Name = "John Doe", Email = "john.doe@example.com" },
            new Application { Id = Guid.NewGuid(), Name = "Jane Smith", Email = "jane.smith@example.com" }
        };
        _mockService.Setup(service => service.GetAllApplicationsAsync()).ReturnsAsync(applications);

        // Act
        var result = await _controller.GetApplications();
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedApplications = Assert.IsType<List<Application>>(okResult.Value);

        // Assert
        Assert.Equal(2, returnedApplications.Count);
    }

    [Fact]
    public async Task GetApplication_ReturnsApplicationById()
    {
        var applicationId = Guid.NewGuid();
        var application = new Application { Id = applicationId, Name = "John Doe", Email = "john.doe@example.com" };
        _mockService.Setup(service => service.GetApplicationByIdAsync(applicationId)).ReturnsAsync(application);

        var result = await _controller.GetApplication(applicationId);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedApplication = Assert.IsType<Application>(okResult.Value);

        Assert.Equal(applicationId, returnedApplication.Id);
        Assert.Equal("John Doe", returnedApplication.Name);
    }

    [Fact]
    public async Task GetApplication_ReturnsNotFound_WhenApplicationDoesNotExist()
    {
        var nonExistentId = Guid.NewGuid();
        _mockService.Setup(service => service.GetApplicationByIdAsync(nonExistentId)).ReturnsAsync((Application)null);

        var result = await _controller.GetApplication(nonExistentId);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostApplication_CreatesNewApplication()
    {
        var application = new Application
        {
            Id = Guid.NewGuid(),
            Name = "Jane Doe",
            Email = "jane.doe@example.com",
            VehicleRegistrationNumber = "ABC123"
        };

        var result = await _controller.PostApplication(application);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);

        Assert.Equal("GetApplication", createdAtActionResult.ActionName);
    }

    [Fact]
    public async Task DeleteApplication_ReturnsNoContent_WhenApplicationIsDeleted()
    {
        // Arrange
        var applicationId = Guid.NewGuid();
        _mockService.Setup(service => service.DeleteApplicationAsync(applicationId)).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteApplication(applicationId);

        // Assert
        Assert.IsType<NoContentResult>(result);  // Expecting 204 No Content
    }

    [Fact]
    public async Task DeleteApplication_ReturnsNotFound_WhenApplicationDoesNotExist()
    {
        // Arrange
        var applicationId = Guid.NewGuid();
        _mockService.Setup(service => service.DeleteApplicationAsync(applicationId)).ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteApplication(applicationId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);  // Expecting 404 Not Found
    }

}
