using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LMS.Shared.DTOs;
using LMS.Shared.DTOs.ModuleDTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleService _moduleService;

        public ModulesController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        /// <summary>Returns all modules.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ModuleDto>), 200)]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetAll(CancellationToken cancellationToken)
        {
            var modules = await _moduleService.GetAllAsync(cancellationToken);
            return Ok(modules);
        }

        /// <summary>Returns one module by id.</summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ModuleDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ModuleDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var dto = await _moduleService.GetByIdAsync(id, cancellationToken);
            return dto is null ? NotFound() : Ok(dto);
        }

        /// <summary>Updates an existing module by id.</summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ModuleDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ModuleDto>> Update(Guid id, [FromBody] UpdateModuleDto dto, CancellationToken cancellationToken)
        {
            var updatedModule = await _moduleService.UpdateModuleAsync(id, dto, cancellationToken);
            return updatedModule is null ? NotFound() : Ok(updatedModule);
        }
    }
}
