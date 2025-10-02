using LMS.Shared.DTOs.ActivityDTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace LMS.Presentation.Controllers;

[ApiController]
[Route("api/modules/{moduleId:guid}/activities")]
public class ModuleActivitiesController : ControllerBase
{
    private readonly IActivityService _service;
    public ModuleActivitiesController(IActivityService service) => _service = service;

    /// <summary>Lists all activities for a module (teacher view).</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ActivityDto>), 200)]
    public async Task<ActionResult<IEnumerable<ActivityDto>>> Get(Guid moduleId, CancellationToken ct)
        => Ok(await _service.GetByModuleAsync(moduleId, ct));
}
