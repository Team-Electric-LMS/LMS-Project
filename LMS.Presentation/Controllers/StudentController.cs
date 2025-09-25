using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using LMS.Shared.DTOs.CourseDTOs;
using LMS.Shared.DTOs.ModuleDTOs;

namespace LMS.Presentation.Controllers
{
    [Route("api/student")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class StudentController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public StudentController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet("{id}/course")]
        public async Task<IActionResult> GetCourseForStudent(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid student ID.");

            try
            {
                var courseDto = await serviceManager.StudentService.GetCourseForStudentAsync(id);

                if (courseDto == null)
                    return NotFound($"No course found for student with id {id}");

                return Ok(courseDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
        // endpoint for getting modules for a student by student id
        // does not include course details, can be used for creating a schedule of modules
        [HttpGet("{id}/modules")]
        public async Task<IActionResult> GetModulesForStudent(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid student ID.");

            try
            {
                var modules = await serviceManager.StudentService.GetModulesForStudentAsync(id);

                if (modules == null || !modules.Any())
                    return NotFound($"No modules found for student with id {id}");

                return Ok(modules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
        // endpoint for getting course + modules for a student
        // combines the two previous endpoints and works as a dashboard overview
        [HttpGet("{id}/course-with-modules")]
        public async Task<IActionResult> GetCourseWithModulesForStudent(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid student ID.");

            try
            {
                var courseWithModules = await serviceManager.StudentService.GetCourseWithModulesForStudentAsync(id);

                if (courseWithModules == null)
                    return NotFound($"No course found for student with id {id}");

                return Ok(courseWithModules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // GET api/student/{studentId}/modules/{moduleId}/activities
        [HttpGet("{studentId}/modules/{moduleId}/activities")]
        public async Task<IActionResult> GetActivitiesForModule(Guid studentId, Guid moduleId)
        {
            if (studentId == Guid.Empty)
                return BadRequest("Invalid student ID.");

            if (moduleId == Guid.Empty)
                return BadRequest("Invalid module ID.");

            try
            {
                var activities = await serviceManager.StudentService.GetActivitiesForModuleAsync(studentId, moduleId);

                if (activities == null || !activities.Any())
                    return NotFound($"No activities found for module {moduleId} and student {studentId}");

                return Ok(activities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

    }
}