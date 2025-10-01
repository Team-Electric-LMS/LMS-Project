using LMS.Shared.DTOs.ActivityDTOs;
using LMS.Shared.DTOs.CourseDTOs;
using LMS.Shared.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    [HttpPost]
    [Authorize(Roles = "Teacher")]
    public async Task<ActionResult<ActivityDto>> CreateActivity([FromBody] CreateActivityDto createActivityDto)
    {
        var activity = await serviceManager.ActivityService.CreateActivityAsync(createActivityDto);
        return Ok(activity);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Teacher")]
    public async Task<ActionResult<ActivityDto>> UpdateActivity(Guid id, [FromBody] UpdateActivityDto updateActivityDto)
    {
        if (id != updateActivityDto.Id) return BadRequest();
        await serviceManager.ActivityService.UpdateActivityAsync(id, updateActivityDto);
        return NoContent();
    }
}




























