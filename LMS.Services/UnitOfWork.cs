using LMS.Infractructure.Data;
using System.Threading.Tasks;

namespace LMS.Services
{
    // Implements the Unit of Work pattern to manage repositories and database transactions.
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICourseRepository Courses { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Courses = new CourseRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
