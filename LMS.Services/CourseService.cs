using LMS.Shared.DTOs;
using Service.Contracts;
using LMS.Infractructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Entities;

namespace LMS.Services;

public class CourseService : ICourseService
{
    private readonly ApplicationDbContext _context;

    public CourseService(ApplicationDbContext context)
    {
        _context = context;
    }
    // Fetch courses by teacher ID
    public async Task<IEnumerable<CourseDto>> GetCoursesByTeacherAsync(Guid teacherId)
    {
        if (teacherId == Guid.Empty)
            throw new ArgumentException("Invalid teacher ID.");
        // Ensure teacher exists, otherwise return null
        try
        {
            var courses = await _context.Courses
                .Where(course => course.Teachers.Any(teacher => teacher.Id == teacherId.ToString()))
                .Select(course => new CourseDto
                {
                    Id = course.Id,
                    Name = course.Name ?? string.Empty,
                    TeacherId = teacherId
                })
                .ToListAsync();

            return courses;
        }
        catch (Exception ex)
        {
            // Log exceptions
            throw new ApplicationException("An error occurred while fetching courses.", ex);
        }
    }
}