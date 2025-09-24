using Domain.Models.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IStudentRepository
    {
        Task<ApplicationUser?> GetStudentWithCourseAsync(Guid studentId);
        Task<ApplicationUser?> GetStudentByIdAsync(Guid studentId);
    }
}
