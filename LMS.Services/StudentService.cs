using LMS.Infractructure.Data;
using LMS.Shared.DTOs.CourseDTOs;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;

namespace LMS.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CourseDto?> GetCourseForStudentAsync(Guid studentId)
        {
            // Hämta studenten
            var student = await _context.Users
                .FirstOrDefaultAsync(s => s.Id == studentId.ToString());

            if (student == null || student.CourseId == null)
            {
                return null;
            }

            // Hämta kursen
            var courseDto = await _context.Courses
                .Where(c => c.Id == student.CourseId)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Name = c.Name ?? string.Empty,
                    Description = c.Description ?? string.Empty,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate
                })
                .FirstOrDefaultAsync();

            return courseDto;
        }
    }
}