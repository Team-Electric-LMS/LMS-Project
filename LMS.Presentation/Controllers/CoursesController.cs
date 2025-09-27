using LMS.Shared.DTOs.CourseDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace LMS.Presentation.Controllers;

[Route("api/courses")]
[ApiController]
[Produces("application/json")]
public class CoursesController(IServiceManager serviceManager) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CourseDto>> GetCourse(Guid id) => Ok(await serviceManager.CourseService.GetCourseAsync(id));

    [HttpPost]
    [Authorize(Roles = "Teacher")]
    public async Task<ActionResult<CourseDto>> CreateCourse(CreateCourseDto createCourseDto)
    {
        var course = await serviceManager.CourseService.CreateCourseAsync(createCourseDto);
        return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Teacher")]
    public async Task<ActionResult<CourseDto>> UpdateCourse(Guid id, UpdateCourseDto updateCourseDto)
    {
        if (id != updateCourseDto.Id) return BadRequest();
        await serviceManager.CourseService.UpdateCourseAsync(id, updateCourseDto);
        return NoContent();
    }
}