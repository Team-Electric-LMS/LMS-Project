using System.Threading.Tasks;

namespace LMS.Services
{
    public interface IUnitOfWork
    {
        ICourseRepository Courses { get; }
        Task<int> SaveChangesAsync();
    }
}
