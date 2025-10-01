using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LMS.Shared.DTOs;
using LMS.Shared.DTOs.ModuleDTOs;

namespace Service.Contracts
{
    public interface IModuleService
    {
        Task<IReadOnlyList<ModuleDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ModuleDto?> GetByIdAsync(Guid moduleId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ModuleDto>> GetByCourseIdAsync(Guid courseId, CancellationToken cancellationToken = default);
        Task<ModuleDto> CreateModuleAsync(CreateModuleDto dto, CancellationToken cancellationToken = default);
        Task<ModuleDto?> UpdateModuleAsync(Guid moduleId, UpdateModuleDto dto, CancellationToken cancellationToken = default);
    }
}
