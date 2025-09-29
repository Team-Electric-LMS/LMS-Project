using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IModuleRepository
    {
        IQueryable<Module> GetAll(bool trackChanges = false);
        IQueryable<Module> GetByCourseId(Guid courseId, bool trackChanges = false);
        Task<Module?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
