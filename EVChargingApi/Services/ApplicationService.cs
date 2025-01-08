using EVChargingAPI.Models;
using EVChargingAPI.Repositories;

namespace EVChargingAPI.Services;
public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _repository;

    public ApplicationService(IApplicationRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Application>> GetAllApplicationsAsync()
    {
        return await _repository.GetAllApplicationsAsync();
    }

    public async Task<Application?> GetApplicationByIdAsync(Guid id)
    {
        return await _repository.GetApplicationByIdAsync(id);
    }

    public async Task SubmitApplicationAsync(Application application)
    {
        application.Timestamp = DateTime.UtcNow;  // Set timestamp
        await _repository.AddApplicationAsync(application);
        await _repository.SaveChangesAsync();
    }
}
