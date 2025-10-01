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
            var modules = await _uow.Modules
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
            // Always return a non-null list
            return modules ?? new List<ModuleDto>();
        }

        public async Task<ModuleDto> CreateModuleAsync(CreateModuleDto dto, CancellationToken cancellationToken = default)
        {
            // Log incoming DTO
            Console.WriteLine($"[CreateModuleAsync] DTO: Name={dto.Name}, Description={dto.Description}, StartDate={dto.StartDate}, EndDate={dto.EndDate}, CourseId={dto.CourseId}");

            // Validate incoming dates
            if (dto.StartDate == DateOnly.MinValue || dto.EndDate == DateOnly.MinValue)
            {
                Console.WriteLine("[CreateModuleAsync] Invalid module dates: StartDate or EndDate is MinValue.");
                throw new ArgumentException("StartDate and EndDate must be valid dates.");
            }

            // Fetch the course
            var course = await _uow.Courses.GetCourseByIdAsync(dto.CourseId, trackChanges: false);
            Console.WriteLine($"[CreateModuleAsync] Fetched course: {(course == null ? "null" : course.Name)}, StartDate={course?.StartDate}, EndDate={course?.EndDate}");
            if (course == null)
                throw new Exception($"Course with id {dto.CourseId} not found.");

            // Validate course dates
            if (course.StartDate == DateOnly.MinValue || course.EndDate == DateOnly.MinValue)
            {
                Console.WriteLine("[CreateModuleAsync] Invalid course dates: StartDate or EndDate is MinValue.");
                throw new Exception($"Course dates are not set. StartDate: {course.StartDate}, EndDate: {course.EndDate}");
            }

            // Validate module dates
            if (dto.StartDate < course.StartDate || dto.EndDate > course.EndDate)
            {
                Console.WriteLine($"[CreateModuleAsync] Module dates out of course period: ModuleStart={dto.StartDate}, ModuleEnd={dto.EndDate}, CourseStart={course.StartDate}, CourseEnd={course.EndDate}");
                throw new Exception("The module dates must be within the course's active period.");
            }

            var module = new Domain.Models.Entities.Module
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CourseId = dto.CourseId
            };
            await _uow.Modules.AddAsync(module, cancellationToken);
            await _uow.CompleteAsync();
            Console.WriteLine($"[CreateModuleAsync] Module created: Id={module.Id}, Name={module.Name}");
            return new ModuleDto
            {
                Id = module.Id,
                Name = module.Name,
                Description = module.Description,
                StartDate = module.StartDate,
                EndDate = module.EndDate,
                CourseId = module.CourseId
            };
        }

        public async Task<ModuleDto?> UpdateModuleAsync(Guid moduleId, UpdateModuleDto dto, CancellationToken cancellationToken = default)
        {
            // Log incoming DTO
            Console.WriteLine($"[UpdateModuleAsync] DTO: Name={dto.Name}, Description={dto.Description}, StartDate={dto.StartDate}, EndDate={dto.EndDate}");

            // Validate incoming dates
            if (dto.StartDate == DateOnly.MinValue || dto.EndDate == DateOnly.MinValue)
            {
                Console.WriteLine("[UpdateModuleAsync] Invalid module dates: StartDate or EndDate is MinValue.");
                throw new ArgumentException("StartDate and EndDate must be valid dates.");
            }

            var module = await _uow.Modules.GetByIdAsync(moduleId, cancellationToken);
            Console.WriteLine($"[UpdateModuleAsync] Fetched module: {(module == null ? "null" : module.Name)}, StartDate={module?.StartDate}, EndDate={module?.EndDate}");
            if (module == null) return null;

            // Fetch the course
            var course = await _uow.Courses.GetCourseByIdAsync(module.CourseId, trackChanges: false);
            Console.WriteLine($"[UpdateModuleAsync] Fetched course: {(course == null ? "null" : course.Name)}, StartDate={course?.StartDate}, EndDate={course?.EndDate}");
            if (course == null)
                throw new Exception($"Course with id {module.CourseId} not found.");

            // Validate course dates
            if (course.StartDate == DateOnly.MinValue || course.EndDate == DateOnly.MinValue)
            {
                Console.WriteLine("[UpdateModuleAsync] Invalid course dates: StartDate or EndDate is MinValue.");
                throw new Exception($"Course dates are not set. StartDate: {course.StartDate}, EndDate: {course.EndDate}");
            }

            // Validate module dates
            if (dto.StartDate < course.StartDate || dto.EndDate > course.EndDate)
            {
                Console.WriteLine($"[UpdateModuleAsync] Module dates out of course period: ModuleStart={dto.StartDate}, ModuleEnd={dto.EndDate}, CourseStart={course.StartDate}, CourseEnd={course.EndDate}");
                throw new Exception("The module dates must be within the course's active period.");
            }

            module.Name = dto.Name;
            module.Description = dto.Description;
            module.StartDate = dto.StartDate;
            module.EndDate = dto.EndDate;
            await _uow.CompleteAsync();
            Console.WriteLine($"[UpdateModuleAsync] Module updated: Id={module.Id}, Name={module.Name}");
            return new ModuleDto
            {
                Id = module.Id,
                Name = module.Name,
                Description = module.Description,
                StartDate = module.StartDate,
                EndDate = module.EndDate,
                CourseId = module.CourseId
            };
        }
    }
}
