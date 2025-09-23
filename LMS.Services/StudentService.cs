using LMS.Infractructure.Data;
using LMS.Shared.DTOs.CourseDTOs;
using LMS.Shared.DTOs.UserDTOs;
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

        public async Task<CourseWithTeachersDto?> GetCourseForStudentAsync(Guid studentId)
        {
            // Hämta studenten
            var student = await _context.Users
                .FirstOrDefaultAsync(s => s.Id == studentId.ToString());

            if (student == null || student.CourseId == null)
            {
                return null;
            }

            // Hämta kursen med lärare
            var course = await _context.Courses
                .Include(c => c.Teachers)
                .FirstOrDefaultAsync(c => c.Id == student.CourseId);

            if (course == null)
                return null;

            var courseDto = new CourseDto
            {
                Id = course.Id,
                Name = course.Name ?? string.Empty,
                Description = course.Description ?? string.Empty,
                StartDate = course.StartDate,
                EndDate = course.EndDate
            };

            var teachers = course.Teachers.Select(t => new LMS.Shared.DTOs.UserDTOs.TeacherDto
            {
                Id = t.Id,
                FirstName = t.FirstName ?? string.Empty,
                LastName = t.LastName ?? string.Empty,
                Email = t.Email ?? string.Empty,
                CoursesTaught = t.CoursesTaught.Select(ct => new CourseDto
                {
                    Id = ct.Id,
                    Name = ct.Name ?? string.Empty,
                    Description = ct.Description ?? string.Empty,
                    StartDate = ct.StartDate,
                    EndDate = ct.EndDate
                }).ToList()
            }).ToList();

            return new CourseWithTeachersDto
            {
                Course = courseDto,
                Teachers = teachers
            };
        }
    }
}