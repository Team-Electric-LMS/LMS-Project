using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LMS.Infractructure.Data;
using LMS.Shared.DTOs;
using LMS.Shared.DTOs.CourseDTOs;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;

namespace LMS.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _db;

        public CourseService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<CourseDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Courses
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<CourseDto> GetByIdAsync(Guid courseId, CancellationToken cancellationToken = default)
        {
            var c = await _db.Courses
                .AsNoTracking()
                .Where(x => x.Id == courseId)
                .Select(x => new CourseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                })
                .FirstOrDefaultAsync(cancellationToken);

            // For simplicity we return an empty DTO if not found (keep current logic minimal)
            return c ?? new CourseDto();
        }

        // Used by GET /api/teachers/{teacherId}/courses
        public async Task<IReadOnlyList<CourseDto>> GetCoursesByTeacherAsync(Guid teacherId, CancellationToken cancellationToken = default)
        {
            var teacherIdString = teacherId.ToString();

            // Read the teacher's CourseId reference(s) from ApplicationUser
            var courseIds = await _db.Users
                .AsNoTracking()
                .Where(u => u.Id == teacherIdString && u.CourseId != null)
                .Select(u => u.CourseId!.Value)
                .ToListAsync(cancellationToken);

            if (courseIds.Count == 0)
                return Array.Empty<CourseDto>();

            var courses = await _db.Courses
                .AsNoTracking()
                .Where(c => courseIds.Contains(c.Id))
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToListAsync(cancellationToken);

            return courses;
        }
    }
}
