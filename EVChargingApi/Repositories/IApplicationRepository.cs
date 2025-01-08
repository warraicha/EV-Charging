using EVChargingAPI.Models;

namespace EVChargingAPI.Repositories;
public interface IApplicationRepository
{
    Task<IEnumerable<Application>> GetAllApplicationsAsync();
    Task<Application?> GetApplicationByIdAsync(Guid id);
    Task AddApplicationAsync(Application application);
    Task<bool> SaveChangesAsync();
}
