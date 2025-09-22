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
            var courseDto = await serviceManager.StudentService.GetCourseForStudentAsync(id);

            if (courseDto == null)
                return NotFound($"No course found for student with id {id}");

            return Ok(courseDto);
        }
    }
}