using Domain.Models.Entities;

namespace LMS.Services
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetCoursesByTeacherAsync(Guid teacherId);
    }
}
