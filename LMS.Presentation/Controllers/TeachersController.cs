using LMS.Shared.DTOs.CourseDTOs;
using LMS.Shared.DTOs.ModuleDTOs;
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

    // POST: api/teachers/courses/{courseId}/modules
    [HttpPost("courses/{courseId}/modules")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> CreateModuleForCourse(Guid courseId, [FromBody] CreateModuleDto dto)
    {
        if (courseId != dto.CourseId)
            return BadRequest("CourseId in route and body must match.");
        var module = await _serviceManager.ModuleService.CreateModuleAsync(dto);
        return CreatedAtAction(nameof(GetModulesForCourse), new { courseId = module.CourseId }, module);
    }

    // PUT: api/teachers/courses/{courseId}/modules/{moduleId}
    [HttpPut("courses/{courseId}/modules/{moduleId}")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> EditModuleForCourse(Guid courseId, Guid moduleId, [FromBody] UpdateModuleDto dto)
    {
        var module = await _serviceManager.ModuleService.GetByIdAsync(moduleId);
        if (module == null || module.CourseId != courseId)
            return NotFound("Module not found for this course.");
        var updated = await _serviceManager.ModuleService.UpdateModuleAsync(moduleId, dto);
        return Ok(updated);
    }
}
