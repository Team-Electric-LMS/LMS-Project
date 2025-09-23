using LMS.Shared.DTOs.CourseDTOs;

namespace Service.Contracts
{
    public interface IStudentService
    {
        Task<CourseWithTeachersDto?> GetCourseForStudentAsync(Guid studentId);
    }
}