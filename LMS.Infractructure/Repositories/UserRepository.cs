using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infractructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> userManager;
    public UserRepository(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }
    public async Task<bool> UserExistsAsync(string id) => await userManager.FindByIdAsync(id) != null;
    public async Task<bool> EmailExistsAsync(string email) => await userManager.FindByEmailAsync(email) != null;
    public async Task<bool> NameExistsAsync(string name) => await userManager.FindByNameAsync(name) != null;

    public async Task<IEnumerable<string>?> GetUsersRolesAsync(ApplicationUser user) => await userManager.GetRolesAsync(user);

    public async Task<ApplicationUser?> GetUserByIdAsync(string id, bool trackChanges = false) => await userManager.FindByIdAsync(id);

    public async Task<ApplicationUser?> GetUserByIdentityNameAsync(string name, bool trackChanges = false) => await userManager.FindByNameAsync(name);

    public async Task<ApplicationUser?> GetUserWithCourseAsync(string? id, string? email, bool trackChanges = false)
    {
        IQueryable<ApplicationUser> query = userManager.Users;

        query = query
            .Include(x => x.CoursesTaught)
            .Include(x => x.Course);

        if (!string.IsNullOrEmpty(id)) return await query.FirstOrDefaultAsync(u => u.Id == id);
        else if (!string.IsNullOrEmpty(email)) return await query.FirstOrDefaultAsync(u => u.Email == email);
        return null;
    }

    public async Task<IEnumerable<ApplicationUser>?> GetAllOrClassmatesAsync(Guid? CourseId)
    {
        var students = await userManager.GetUsersInRoleAsync("Student");
        students = CourseId == null ? students : students.Where(u => u.CourseId == CourseId).ToList();
        return students;

    }
}


