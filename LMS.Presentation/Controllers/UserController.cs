using LMS.Shared.DTOs;
using LMS.Shared.DTOs.ForFrontEndTemplate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Swashbuckle.AspNetCore.Annotations;

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
    [HttpGet("{id}")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Get info on a LMS user for authenticated users",
        Description = "Returns data on user. Requires a valid JWT token and a string Id")]
    [SwaggerResponse(StatusCodes.Status200OK, "A user data", typeof(IEnumerable<UserDto>))]
    //[SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized - JWT token missing or invalid")]

    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDto>> GetMovie(string id) =>
         Ok((UserDto?)await serviceManager.UserService.GetUserByIdAsync(id));

}
