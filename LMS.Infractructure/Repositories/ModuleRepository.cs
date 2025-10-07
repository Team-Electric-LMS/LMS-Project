using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Contracts.Repositories;
using Domain.Models;
using Domain.Models.Entities;
using LMS.Infractructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infractructure.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly ApplicationDbContext _db;

        public ModuleRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<Module> GetAll(bool trackChanges = false)
            => trackChanges ? _db.Modules : _db.Modules.AsNoTracking();

        public IQueryable<Module> GetByCourseId(Guid courseId, bool trackChanges = false)
            => (trackChanges ? _db.Modules : _db.Modules.AsNoTracking())
                .Where(m => m.CourseId == courseId);

        public Task<Module?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => _db.Modules.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        public async Task AddAsync(Module module, CancellationToken cancellationToken = default)
        {
            await _db.Modules.AddAsync(module, cancellationToken);
        }
    }
}
