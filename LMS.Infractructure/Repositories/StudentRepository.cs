using Domain.Contracts.Repositories;
using LMS.Infractructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Entities;

namespace LMS.Infractructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser?> GetStudentWithCourseAsync(Guid studentId)
        {
            return await _context.Users
                .Include(u => u.Course)
                    .ThenInclude(c => c.Modules)
                .FirstOrDefaultAsync(u => u.Id == studentId.ToString());
        }

        public async Task<ApplicationUser?> GetStudentByIdAsync(Guid studentId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == studentId.ToString());
        }

        public async Task<Module?> GetModuleWithActivitiesAsync(Guid moduleId)
        {
            return await _context.Modules
                .Include(m => m.Activities)
                .ThenInclude(a => a.ActivityType)
                .FirstOrDefaultAsync(m => m.Id == moduleId);
        }
    }
}
