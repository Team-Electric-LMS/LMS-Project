using Domain.Models.Entities;

namespace LMS.Services
{
    // Repository interface for managing Course entities.
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetCoursesByTeacherAsync(Guid teacherId);
    }
}
