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
                .FirstOrDefaultAsync(u => u.Id == studentId.ToString());
        }
    }
}
