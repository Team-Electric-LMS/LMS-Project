using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LMS.Shared.DTOs.ActivityDTOs;

namespace Service.Contracts
{
    public interface IActivityService
    {
        Task<IReadOnlyList<ActivityDto>> GetByModuleIdAsync(Guid moduleId, CancellationToken cancellationToken = default);
    }
}
