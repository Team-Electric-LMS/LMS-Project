using LMS.Shared.DTOs;
using Service.Contracts;
using Domain.Models.Entities;

namespace LMS.Services;

public class CourseService : ICourseService
{
    private readonly IUnitOfWork _unitOfWork;

    public CourseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    // Fetch courses by teacher ID
    public async Task<IEnumerable<CourseDto>> GetCoursesByTeacherAsync(Guid teacherId)
    {
        if (teacherId == Guid.Empty)
            throw new ArgumentException("Invalid teacher ID.");
        // Ensure teacher exists, otherwise return null
        try
        {
            var courses = await _unitOfWork.Courses.GetCoursesByTeacherAsync(teacherId);
            return courses.Select(course => new CourseDto
            {
                Id = course.Id,
                Name = course.Name ?? string.Empty,
                TeacherId = teacherId
            });
        }
        catch (Exception ex)
        {
            // Log exceptions
            throw new ApplicationException("An error occurred while fetching courses.", ex);
        }
    }
}