using EVChargingAPI.Models;
using EVChargingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EVChargingAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApplicationsController : ControllerBase
{
    private readonly IApplicationService _applicationService;

    public ApplicationsController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    // GET: api/Applications
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Application>>> GetApplications()
    {
        var applications = await _applicationService.GetAllApplicationsAsync();
        return Ok(applications);
    }

    // GET: api/Applications/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Application>> GetApplication(Guid id)
    {
        var application = await _applicationService.GetApplicationByIdAsync(id);

        if (application == null)
        {
            return NotFound();
        }

        return Ok(application);
    }

    // POST: api/Applications
    [HttpPost]
    public async Task<ActionResult<Application>> PostApplication(Application application)
    {
        await _applicationService.SubmitApplicationAsync(application);
        return CreatedAtAction(nameof(GetApplication), new { id = application.Id }, application);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteApplication(Guid id)
    {
        var result = await _applicationService.DeleteApplicationAsync(id);
        if (!result)
        {
            return NotFound(new { Message = $"Application with ID {id} not found." });
        }
        return NoContent();
    }
}
