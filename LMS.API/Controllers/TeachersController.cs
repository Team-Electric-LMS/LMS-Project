using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Domain.Models.Entities;
using LMS.Shared.Dtos;

namespace LMS.API.Controllers
{
    /// <summary>
    /// Endpoints for retrieving teacher information.
    /// This example treats teachers as ApplicationUser instances that belong to the 'Teacher' role.
    /// </summary>
    [ApiController]
    [Route("api/teachers")]
    public class TeachersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public TeachersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Returns a list of users that are in the 'Teacher' role.
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetTeachers()
        {
            // Note: UserManager.Users returns IQueryable<ApplicationUser>
            var users = _userManager.Users.ToList();
            var teachers = new List<TeacherDto>();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Teacher"))
                {
                    teachers.Add(new TeacherDto
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email
                    });
                }
            }

            return Ok(teachers);
        }
    }
}
