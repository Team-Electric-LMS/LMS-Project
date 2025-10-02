using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LMS.Shared.DTOs.ActivityDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace LMS.Presentation.Controllers
{
    [ApiController]
    [Route("api/modules/{moduleId:guid}/activities")]
    [Authorize(Roles = "Teacher")]
    public class ModuleActivitiesController : ControllerBase
    {
        private readonly IActivityService _activities;

        public ModuleActivitiesController(IActivityService activities) => _activities = activities;

        /// <summary>Lists all activities for a specific module, sorted by StartDate (desc).</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ActivityDto>), 200)]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetActivities(Guid moduleId, CancellationToken cancellationToken = default)
        {
            var list = await _activities.GetByModuleIdAsync(moduleId, cancellationToken);
            return Ok(list);
        }
    }
}
