using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Contracts.Repositories;
using LMS.Shared.DTOs;
using LMS.Shared.DTOs.ModuleDTOs;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;

namespace LMS.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IUnitOfWork _uow;

        public ModuleService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IReadOnlyList<ModuleDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _uow.Modules
                .GetAll(trackChanges: false)
                .OrderBy(m => m.StartDate)
                .Select(m => new ModuleDto
                {
                    Id = m.Id,
                    CourseId = m.CourseId,
                    Name = m.Name,
                    Description = m.Description,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<ModuleDto?> GetByIdAsync(Guid moduleId, CancellationToken cancellationToken = default)
        {
            return await _uow.Modules
                .GetAll(trackChanges: false)
                .Where(m => m.Id == moduleId)
                .Select(m => new ModuleDto
                {
                    Id = m.Id,
                    CourseId = m.CourseId,
                    Name = m.Name,
                    Description = m.Description,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<ModuleDto>> GetByCourseIdAsync(Guid courseId, CancellationToken cancellationToken = default)
        {
            return await _uow.Modules
                .GetByCourseId(courseId, trackChanges: false)
                .OrderBy(m => m.StartDate)
                .Select(m => new ModuleDto
                {
                    Id = m.Id,
                    CourseId = m.CourseId,
                    Name = m.Name,
                    Description = m.Description,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate
                })
                .ToListAsync(cancellationToken);
        }
    }
}
