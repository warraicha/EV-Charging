using EVChargingAPI.Models;

namespace EVChargingAPI.Services;
public interface IApplicationService
{
    Task<IEnumerable<Application>> GetAllApplicationsAsync();
    Task<Application?> GetApplicationByIdAsync(Guid id);
    Task SubmitApplicationAsync(Application application);
    Task<bool> DeleteApplicationAsync(Guid id);
}
