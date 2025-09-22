using Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Entities;
using LMS.Infractructure.Data;

namespace LMS.Infractructure.Repositories
{
    // Repository for managing Course entities.
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetCoursesByTeacherAsync(Guid teacherId)
        {
            return await _context.Courses
                .Where(course => course.Teachers.Any(teacher => teacher.Id == teacherId.ToString()))
                .ToListAsync();
        }
    }
}
