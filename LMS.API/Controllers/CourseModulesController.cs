using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/courses/{courseId:guid}/modules")]
    public class CourseModulesController : ControllerBase
    {
        private readonly IModuleService _moduleService;

        public CourseModulesController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        /// <summary>
        /// Lists all modules that belong to a specific course.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ModuleDto>), 200)]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetModules(Guid courseId, CancellationToken cancellationToken)
        {
            var modules = await _moduleService.GetByCourseIdAsync(courseId, cancellationToken);
            return Ok(modules);
        }
    }
}
