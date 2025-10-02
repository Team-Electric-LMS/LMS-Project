using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IActivityRepository
    {
        Task<IReadOnlyList<Activity>> GetByModuleIdAsync(Guid moduleId, CancellationToken cancellationToken = default);
    }
}
