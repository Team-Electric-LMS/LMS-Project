using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using LMS.Infractructure.Data;
using LMS.Shared.Parameters;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infractructure.Repositories
{
    // Course repository implementation
    public class CourseRepository(ApplicationDbContext context) : RepositoryBase<Course>(context), ICourseRepository
    {
        // Get all courses

        public async Task<IEnumerable<Course>> GetAllAsync(bool trackchanges = false) => await FindAll(trackchanges).ToListAsync();
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

        public async Task<IEnumerable<Course>> GetActiveCoursesExtendedAsync(bool trackChanges = false)
        {
            var query = context.Courses.AsQueryable();
            if (!trackChanges)
                query = query.AsNoTracking();

            query = query.Include(c => c.Modules)
                    .ThenInclude(m => m.Activities).Where(c => c.EndDate >= DateOnly.FromDateTime(DateTime.UtcNow)).OrderBy(d => d.StartDate);

            return await query.ToListAsync();
        }

        public async Task<PagedList<Course>> GetAllPagedAsync(CourseParameters parameters, bool trackChanges = false)
        {
            var query = FindAll(trackChanges);

            if (!string.IsNullOrWhiteSpace(parameters.Name))
                query = query.Where(m => m.Name == parameters.Name.Trim());

            if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
                query = query.Where(m =>
                    m.Name.Contains(parameters.SearchQuery.Trim()));

            if (parameters.StartDate.HasValue)
                query = query.Where(m => m.EndDate >= parameters.StartDate.Value);
            if (parameters.EndDate.HasValue)
                query = query.Where(m => m.EndDate <= parameters.EndDate.Value);

            query = query.OrderBy(m => m.StartDate);

            return await PagedList<Course>.PageAsync(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}