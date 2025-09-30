using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using LMS.Infractructure.Data;

namespace LMS.Infractructure.Repositories
{
    // Course repository implementation
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext context;
        public CourseRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        // Get all courses
        public async Task<IEnumerable<Course>> GetCoursesByTeacherAsync(Guid teacherId)
        {
            return await context.Courses
                .Where(course => course.Teachers.Any(teacher => teacher.Id == teacherId.ToString()))
                .ToListAsync();
        }
        // Get course by id with related entities
        public async Task<Course?> GetCourseByIdAsync(Guid id, bool trackChanges = false)
        {
            var query = context.Courses.AsQueryable();
            if (!trackChanges)
                query = query.AsNoTracking();

            return await query
                .Include(c => c.Modules)
                .Include(c => c.Documents)
                .Include(c => c.Students)
                .Include(c => c.Teachers)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course?> GetCourseWithTeachersAsync(Guid courseId)
        {
            return await context.Courses
                .Include(c => c.Teachers)
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }
        public async Task<Course?> GetCourseWithStudentsAsync(Guid courseId)
        {
            return await context.Courses
                .Include(c => c.Students) // This loads the Students collection
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }

    }
}