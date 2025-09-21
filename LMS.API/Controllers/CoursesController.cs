using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LMS.Infractructure.Data; // Adjust namespace if your project uses LMS.Infrastructure
using LMS.Shared.Dtos;

namespace LMS.API.Controllers
{
    /// <summary>
    /// Endpoints for managing courses and related modules.
    /// </summary>
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a list of all courses in the system.
        /// </summary>
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<CourseDto>> GetCourses()
        {
            // Project courses to DTOs to avoid exposing entity types directly
            var courses = _context.Courses
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToList();

            return Ok(courses);
        }

        /// <summary>
        /// Returns all modules that belong to a specific course.
        /// </summary>
        [HttpGet("{courseId:guid}/modules")]
        [Authorize]
        public ActionResult<IEnumerable<ModuleDto>> GetModules(Guid courseId)
        {
            // Check that the course exists
            var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
            {
                return NotFound();
            }

            // Select modules for the course and project to DTOs
            var modules = _context.Modules
                .Where(m => m.CourseId == courseId)
                .Select(m => new ModuleDto
                {
                    Id = m.Id,
                    Name = m.Name!,
                    Description = m.Description,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    CourseId = m.CourseId
                })
                .ToList();

            return Ok(modules);
        }
    }
}
