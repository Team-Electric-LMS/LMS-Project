using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories;

public interface IUserRepository
{
    Task<bool> UserExistsAsync(string id);
    Task<ApplicationUser?> GetUserByIdAsync(string id, bool trackChanges);
    Task<ApplicationUser?> GetUserWithCourseAsync(string id, bool trackChanges = false);
    Task<IEnumerable<string>?> GetUsersRolesAsync(ApplicationUser user);
    Task<IEnumerable<ApplicationUser>?> GetAllOrClassmatesAsync(Guid? CourseId);
}
