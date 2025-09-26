using LMS.Shared.DTOs.AuthDtos;
using LMS.Shared.DTOs.CourseDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace LMS.Presentation.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IServiceManager serviceManager;

    public AuthController(IServiceManager serviceManager)
    {
        this.serviceManager = serviceManager;
    }

    [HttpPost]
    [Authorize(Roles = "Teacher")]
    [SwaggerOperation(
        Summary = "Register a new user",
        Description = "Creates a new user account with the provided registration details."
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "User successfully registered")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input or registration failed")]
    public async Task<IActionResult> RegisterUser(UserRegistrationDto userRegistrationDto)
    {
        IdentityResult result = await serviceManager.AuthService.RegisterUserAsync(userRegistrationDto);
        if (!result.Succeeded) return BadRequest(result.Errors);
        var user = await serviceManager.UserService.GetUserByIdentityName(userRegistrationDto.UserName);
        return Created($"api/users/{user.Id}", user);
    }

    [HttpGet("check-email/{email}")]
    [Authorize(Roles = "Teacher")]
    [SwaggerOperation(
       Summary = "Check if email registered in database",
       Description = "Checks if a user with such email is already registered.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
    public async Task<IActionResult> CheckEmail(string email) => Ok(await serviceManager.UserService.EmailExistsAsync(email));

    [HttpGet("check-username/{username}")]
    [Authorize(Roles = "Teacher")]
    [SwaggerOperation(
       Summary = "\"Check if username registered in database",
       Description = "Checks if a user with such username is already registered.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
    public async Task<IActionResult> CheckUserName(string email) => Ok(await serviceManager.UserService.UserNameExistsAsync(email));


    [HttpPost("edit")]
    [Authorize(Roles = "Teacher")]
    [SwaggerOperation(
        Summary = "Edit an existing user",
        Description = "Edits an existing user account with the provided details."
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "User data successfully edited")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input or update failed")]
    public async Task<IActionResult> EditUser(UserUpdateDto userEditDto)
    {
        IdentityResult result = await serviceManager.AuthService.UpdateUserAsync(userEditDto);
        if (!result.Succeeded) return BadRequest(result.Errors);
        var user = await serviceManager.UserService.GetUserByIdAsync(userEditDto.Id);
        return Created($"api/users/{user.Id}", user);
    }



    [HttpPost("login")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Authenticate user",
        Description = "Validates user credentials and returns a JWT token for authorization."
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Authentication successful", typeof(TokenDto))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Invalid username or password")]
    public async Task<IActionResult> Authenticate(UserAuthDto user)
    {
        if (!await serviceManager.AuthService.ValidateUserAsync(user))
            return Unauthorized();

        var tokenDto = await serviceManager.AuthService.CreateTokenAsync(addTime: true);
        return Ok(tokenDto);
    }

    [HttpGet("user")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Return Users Id, email, Name via User claims",
        Description = "Returns claims for UI experience and routing"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Claims received", typeof(TokenDto))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "No Access")]
    public async Task<IActionResult> GetCurrent()
    {
        /*var claimsInfo = User.Claims.Select(c => new
        {
            Type = c.Type,
            Value = c.Value
        }).ToList(); */

        var claims = new
        {
            Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            Email = User.FindFirst(ClaimTypes.Name)?.Value,
            Role = User.FindFirst(ClaimTypes.Role)?.Value,
            Name = User.FindFirst(ClaimTypes.GivenName)?.Value
        };

        return Ok(claims);
    }


}
