using LMS.Shared.DTOs.CourseDTOs;
using LMS.Shared.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

namespace LMS.Presentation.Controllers;

[Route("api/courses")]
[ApiController]
[Produces("application/json")]
public class CoursesController(IServiceManager serviceManager) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Teacher")]
    [SwaggerOperation(
     Summary = "Get all courses",
     Description = "Returns all courses, no filters")]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetAlCoursesAsync() => Ok((IEnumerable<CourseDto>)await serviceManager.CourseService.GetAllCoursesAsync());

    [HttpGet("search")]
    [Authorize(Roles = "Teacher")]
    public async Task<ActionResult<IEnumerable<CourseDto>>> SearchCoursesAsync([FromQuery] string? query) => Ok(await serviceManager.CourseService.SearchCourseByNameAsync(query ?? string.Empty));

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

    [HttpGet("courses-tree")]
    [Authorize(Roles = "Teacher")]
    [SwaggerOperation(
        Summary = "Gets Course-Modules-Activities data",
        Description = "Gets a json with Course-Modules-Activities data for courses which en date has not yet passsed.")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized - JWT token missing or invalid")]
    public async Task<ActionResult<IEnumerable<CourseIdNameDto>>> GetActiveCoursesTree()
    {
        var courses = await serviceManager.CourseService.GetActiveCoursesExtendedAsync();
        return Ok(courses);
    }

    [HttpGet("archive")]
    [Authorize(Roles = "Teacher")]
    [SwaggerOperation(
    Summary = "Retrieve paged list of courses",
    Description = @"Returns all courses with optional pagination and filtering.
        Supports:
        - Exact match filtering by `Name`
        - Partial search via `SearchQuery`
        - Filtering by `StartDate` and `EndDate`
        - Pagination metadata in the `X-Pagination` response header (TotalCount, CurrentPage, PageSize, TotalPages, HasNext, HasPrevious)")]

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CourseDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesPagedAsync([FromQuery] CourseParameters parameters)
    {
        try
        {
            var courses = await serviceManager.CourseService.GetCoursesPagedAsync(parameters);
            var paginationMetadata = new { courses.TotalCount, courses.CurrentPage, courses.PageSize, courses.TotalPages, courses.HasNext, courses.HasPrevious };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");


            return Ok(courses);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An unexpected error occurred.", detail = ex.Message });
        }
    }
}

