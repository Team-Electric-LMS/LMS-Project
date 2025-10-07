using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories;

public interface IActivityRepository : IRepositoryBase<Activity>
{
    Task<List<Activity>> GetByModuleIdAsync(Guid moduleId, CancellationToken ct = default);
    Task<ActivityType?> GetTypeByNameAsync(string name, CancellationToken ct = default);
}