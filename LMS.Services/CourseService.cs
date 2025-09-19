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

    public async Task<IEnumerable<CourseDto>> GetCoursesByTeacherAsync(Guid teacherId)
    {
        if (teacherId == Guid.Empty)
            throw new ArgumentException("Invalid teacher ID.");
        try
        {
            var courses = await _context.Courses
                .Where(c => c.Teachers.Any(t => t.Id == teacherId.ToString()))
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    TeacherId = teacherId
                })
                .ToListAsync();

            return courses ?? new List<CourseDto>();
        }
        catch (Exception ex)
        {
            // Log the exception (inject ILogger<CourseService> and use it here)
            throw new ApplicationException("An error occurred while fetching courses.", ex);
        }
    }
}