using LMS.Shared.DTOs.ActivityDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace LMS.Presentation.Controllers;

[Route("api/activities")]
[ApiController]
public class ActivityController : ControllerBase
{
    private readonly IServiceManager serviceManager;

    public ActivityController(IServiceManager serviceManager)
    {
        this.serviceManager = serviceManager;
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<ActivityDto>> GetActivity(Guid id) => Ok(await serviceManager.ActivityService.GetActivityAsync(id));

    [HttpGet("module/{moduleId:guid}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<UpdateActivityDto>>> GetActivities(Guid moduleId, CancellationToken ct)
       => Ok(await serviceManager.ActivityService.GetByModuleIdAsync(moduleId, ct));


    [HttpPost]
    [Authorize(Roles = "Teacher")]
    public async Task<ActionResult<ActivityDto>> CreateActivity([FromBody] CreateActivityDto createActivityDto)
    {
        var activity = await serviceManager.ActivityService.CreateActivityAsync(createActivityDto);
        return Ok(activity);
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<ActivityDto>> UpdateActivity([FromBody] UpdateActivityDto updateActivityDto)
    {
        await serviceManager.ActivityService.UpdateActivityAsync(updateActivityDto);
        return NoContent();
    }
}