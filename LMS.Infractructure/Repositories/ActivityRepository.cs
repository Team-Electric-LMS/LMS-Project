using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Contracts.Repositories;
using Domain.Models;
using Domain.Models.Entities;
using LMS.Infractructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly ApplicationDbContext _db;

        public ActivityRepository(ApplicationDbContext db) => _db = db;

        public async Task<IReadOnlyList<Activity>> GetByModuleIdAsync(Guid moduleId, CancellationToken cancellationToken = default)
        {
            return await _db.Activities
                .AsNoTracking()
                .Where(a => a.ModuleId == moduleId)
                .OrderByDescending(a => a.StartDate)
                .ToListAsync(cancellationToken);
        }
    }
}
