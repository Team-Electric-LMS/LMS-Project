using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LMS.Shared.DTOs;

namespace Service.Contracts
{
    public interface IModuleService
    {
        /// <summary>
        /// Returns all modules that belong to the given course.
        /// </summary>
        Task<IReadOnlyList<ModuleDto>> GetByCourseIdAsync(Guid courseId, CancellationToken cancellationToken = default);
    }
}
