using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using LMS.Infractructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infractructure.Repositories;

// public class ActivityRepository(ApplicationDbContext context) : RepositoryBase<Activity>(context), IActivityRepository
// {
// }

public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository
{
    private readonly ApplicationDbContext _db;

    public ActivityRepository(ApplicationDbContext db) : base(db)
        => _db = db;

    public Task<List<Activity>> GetByModuleIdAsync(Guid moduleId, CancellationToken ct = default)
        => _db.Activities
             .AsNoTracking()
             .Include(a => a.ActivityType)
             .Where(a => a.ModuleId == moduleId)
             .OrderBy(a => a.StartDate)
             .ToListAsync(ct);

    public async Task<ActivityType?> GetTypeByNameAsync(string name, CancellationToken ct = default)
    => await _db.ActivityTypes
        .AsNoTracking()
        .FirstOrDefaultAsync(t => t.Name == name, ct);
}