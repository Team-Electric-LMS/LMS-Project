using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LMS.Infractructure.Data;
using LMS.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;

namespace LMS.Services
{
    public class ModuleService : IModuleService
    {
        private readonly ApplicationDbContext _db;

        public ModuleService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<ModuleDto>> GetByCourseIdAsync(Guid courseId, CancellationToken cancellationToken = default)
        {
            // Keep it simple: project directly to DTOs using EF Core
            var query = _db.Modules
                .AsNoTracking()
                .Where(m => m.CourseId == courseId)
                .OrderBy(m => m.StartDate)
                .Select(m => new ModuleDto
                {
                    Id = m.Id,
                    CourseId = m.CourseId,
                    Name = m.Name,
                    Description = m.Description,
                    StartDate = m.StartDate.ToDateTime(TimeOnly.MinValue),
                    EndDate = m.EndDate.ToDateTime(TimeOnly.MinValue)
                });

            return await query.ToListAsync(cancellationToken);
        }
    }
}
