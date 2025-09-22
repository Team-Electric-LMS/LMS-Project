using LMS.Shared.DTOs;

namespace Service.Contracts;
// Service contract for course-related operations
public interface ICourseService
{
    Task<IEnumerable<CourseDto>> GetCoursesByTeacherAsync(Guid teacherId);
}
