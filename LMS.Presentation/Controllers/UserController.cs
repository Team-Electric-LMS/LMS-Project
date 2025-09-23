using LMS.Shared.DTOs.ForFrontEndTemplate;
using LMS.Shared.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace LMS.Presentation.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IServiceManager serviceManager;

    public UserController(IServiceManager serviceManager)
    {
        this.serviceManager = serviceManager;
    }

    // GET: api/users/{id}
    [HttpGet("{UserId}")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Get User info on a LMS user for authenticated users",
        Description = "Returns data on Student or Teacher, with or without an extended info on assigned course/s" +
        ". Requires a valid JWT token and a string Id")]
    [SwaggerResponse(StatusCodes.Status200OK, "A user data", typeof(IEnumerable<StudentDto>))]
    //[SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized - JWT token missing or invalid")]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDto>> GetUserAsync(string userId, bool includeCourse=true) =>
         Ok(await serviceManager.UserService.GetUserByIdAsync(userId, includeCourse));

    [HttpGet("username/{UserName}")]
    [Authorize(Roles ="Teacher")]
    [SwaggerOperation(
     Summary = "Get User info on a LMS user for authenticated users",
     Description = "Returns data on Student or Teacher, with or without an extended info on assigned course/s" +
     ". Requires a valid JWT token and a string Id")]
    [SwaggerResponse(StatusCodes.Status200OK, "A user data", typeof(IEnumerable<StudentDto>))]
    //[SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized - JWT token missing or invalid")]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDto>> GetUserByNameAsync(string UserName, bool includeCourse = true) =>
      Ok(await serviceManager.UserService.GetUserByIdentityName(UserName, includeCourse));



    // GET: api/users/class/{id}
    [HttpGet("class/{courseId:Guid?}")]
    [Authorize]
    [SwaggerOperation(
       Summary = "Get info with email on all students from school or class, for authenticated users",
       Description = "Returns data with email on all Students or studens of one class if ClassId is provided." +
       ". Requires a valid JWT token")]
    [SwaggerResponse(StatusCodes.Status200OK, "A user data", typeof(IEnumerable<UserDto>))]
    //[SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized - JWT token missing or invalid")]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<StudentDto>> GetClassmates(Guid? courseId) =>
        Ok(await serviceManager.UserService.GetStudents(courseId));



    // POST /users/{id}/assign/ 
    [HttpPost("{userId:Guid}/assign")]
    [SwaggerOperation(Summary = "Assign course to a user", Description = "Assign course to an existing User (Id)")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AddCourseToUser(string userId, [FromBody] AddUserCourseByIdDto? dto, [FromQuery] bool unassign = false)
    {
        //ArgumentNullException.ThrowIfNull(dto);
        await serviceManager.UserService.UpdateUserCourseAsync(userId, unassign, dto);
        return NoContent();
    }

}


