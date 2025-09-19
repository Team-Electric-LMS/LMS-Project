using System.Threading.Tasks;

namespace LMS.Services
{
    // Interface for the Unit of Work pattern to manage repositories and database transactions.
    public interface IUnitOfWork
    {
        ICourseRepository Courses { get; }
        Task<int> SaveChangesAsync();
    }
}
