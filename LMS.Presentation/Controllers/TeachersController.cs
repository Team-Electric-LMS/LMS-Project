using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace LMS.Presentation.Controllers;

[ApiController]
[Route("api/teachers")]
public class TeachersController : ControllerBase
{
    private readonly ICourseService _courseService;

    public TeachersController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet("{teacherId}/courses")]
    // TODO FRONTEND: frontend must send the token in the Authorization: Bearer <token> header.
    // Only authenticated users with the Teacher role
    [Authorize(Roles = "Teacher")] 
    public async Task<IActionResult> GetCoursesForTeacher(Guid teacherId)
    {
        try
        {
            var courses = await _courseService.GetCoursesByTeacherAsync(teacherId);
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
}
