using LMS.Infractructure.Data;
using LMS.Shared.DTOs;
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
                    CourseName = c.Name,
                    Description = c.Description,
                    StartDate = c.StartDate.ToDateTime(TimeOnly.MinValue),
                    EndDate = c.EndDate.ToDateTime(TimeOnly.MinValue),
                    TeacherName = c.Teachers
                          .Select(t => t.FirstName + " " + t.LastName)
                          .FirstOrDefault() ?? string.Empty
                })
                .FirstOrDefaultAsync();

            return courseDto;
        }
    }
}