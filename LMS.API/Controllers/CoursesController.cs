using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LMS.Shared.DTOs;
using LMS.Shared.DTOs.CourseDTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        /// <summary>List all courses.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CourseDto>), 200)]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll(CancellationToken cancellationToken)
        {
            var courses = await _courseService.GetAllAsync(cancellationToken);
            return Ok(courses);
        }

        /// <summary>Get one course by id.</summary>
        [HttpGet("{courseId:guid}")]
        [ProducesResponseType(typeof(CourseDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CourseDto>> GetById(Guid courseId, CancellationToken cancellationToken)
        {
            var course = await _courseService.GetByIdAsync(courseId, cancellationToken);
            if (course is null) return NotFound();
            return Ok(course);
        }
    }
}
