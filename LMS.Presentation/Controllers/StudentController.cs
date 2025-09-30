using LMS.Shared.DTOs.CourseDTOs;
using LMS.Shared.DTOs.ModuleDTOs;
using LMS.Shared.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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
        [HttpGet("{id}/course/students")]
        public async Task<IActionResult> GetCoursemates(Guid id)
        {
            IEnumerable<StudentDto> studentDto = await serviceManager.StudentService.GetCoursematesAsync(id);
            if (studentDto == null)
                return NotFound($"No course mates found for student with id {id}");
            return Ok(studentDto);

        }
    }
}
