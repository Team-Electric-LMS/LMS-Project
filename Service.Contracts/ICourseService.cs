using LMS.Shared.DTOs.CourseDTOs;

namespace Service.Contracts;
// Service contract for course-related operations
public interface ICourseService
{
    Task<CourseDto> GetCourseAsync(Guid id);
    Task<IEnumerable<CourseDto>> GetCoursesByTeacherAsync(Guid teacherId);
    Task<CourseDto> CreateCourseAsync(CreateCourseDto createCourseDto);
    Task UpdateCourseAsync(Guid id, UpdateCourseDto updateCourseDto);
}
