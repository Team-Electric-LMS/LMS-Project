using LMS.Shared.DTOs.CourseDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace LMS.Presentation.Controllers;

// Controller for managing teacher-related endpoints
[ApiController]
[Route("api/teachers")]
public class TeachersController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public TeachersController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    // GET: api/teachers/{teacherId}/courses
    [HttpGet("{teacherId}/courses")]
    [Authorize(Roles = "Teacher")]
    // Fetch courses for a specific teacher by their ID
    public async Task<IActionResult> GetCoursesForTeacher(Guid teacherId)
    {
        try
        {
            var courses = await _serviceManager.CourseService.GetCoursesByTeacherAsync(teacherId);
            if (courses == null)
                return NotFound(new { message = "Teacher not found or invalid ID." });
            if (!courses.Any())
                return Ok(new List<CourseDto>()); // Return empty list if teacher has no courses
            return Ok(courses);
        }
        catch (Exception ex)
        {
            // Log the error
            return StatusCode(500, new { message = "An unexpected error happened, probably runtime related. Please check logs.", details = ex.Message });
        }
    }

    // GET: api/teachers/courses/{courseId}/modules
    [HttpGet("courses/{courseId}/modules")]
    [Authorize(Roles = "Teacher")]
    // Fetch all modules belonging to a specific course
    public async Task<IActionResult> GetModulesForCourse(Guid courseId)
    {
        try
        {
            var modules = await _serviceManager.ModuleService.GetByCourseIdAsync(courseId);
            if (modules == null || !modules.Any())
                return Ok(new List<object>()); // Return empty list if no modules
            return Ok(modules);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An unexpected error happened, probably runtime related. Please check logs.", details = ex.Message });
        }
    }
}
