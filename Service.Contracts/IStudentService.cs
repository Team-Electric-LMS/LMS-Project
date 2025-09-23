using LMS.Shared.DTOs;

namespace Service.Contracts
{
    public interface IStudentService
    {
        Task<CourseDto?> GetCourseForStudentAsync(Guid studentId);
    }
}