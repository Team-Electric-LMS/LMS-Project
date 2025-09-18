using LMS.Shared.DTOs;

namespace Service.Contracts;

public interface ICourseService
{
    Task<IEnumerable<CourseDto>> GetCoursesByTeacherAsync(Guid teacherId);
}
