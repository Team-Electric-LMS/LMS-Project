using LMS.Shared.DTOs.CourseDTOs;

namespace Service.Contracts
{
    public interface IStudentService
    {
        Task<CourseDto?> GetCourseForStudentAsync(Guid studentId);
    }
}