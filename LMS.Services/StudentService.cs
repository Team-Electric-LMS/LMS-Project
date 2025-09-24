using Domain.Contracts.Repositories;
using LMS.Infractructure.Data;
using LMS.Infractructure.Repositories;
using LMS.Shared.DTOs.CourseDTOs;
using LMS.Shared.DTOs.UserDTOs;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;

namespace LMS.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<CourseWithTeachersDto?> GetCourseForStudentAsync(Guid studentId)
        {
            var student = await unitOfWork.Students.GetStudentByIdAsync(studentId);
            if (student == null || student.CourseId == null)
            {
                return null;
            }

            var course = await unitOfWork.Courses.GetCourseWithTeachersAsync(student.CourseId.Value);
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