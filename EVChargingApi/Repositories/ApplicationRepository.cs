using EvChargingAPI.Data;
using EVChargingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EVChargingAPI.Repositories;
public class ApplicationRepository : IApplicationRepository
{
    private readonly ApplicationDbContext _context;

    public ApplicationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Application>> GetAllApplicationsAsync()
    {
        return await _context.Applications.Include(a => a.Address).ToListAsync();
    }

    public async Task<Application?> GetApplicationByIdAsync(Guid id)
    {
        return await _context.Applications.Include(a => a.Address).FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddApplicationAsync(Application application)
    {
        await _context.Applications.AddAsync(application);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
