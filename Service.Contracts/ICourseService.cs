using LMS.Shared.DTOs.CourseDTOs;
using LMS.Shared.Parameters;

namespace Service.Contracts;
// Service contract for course-related operations
public interface ICourseService
{
    Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
    Task<CourseDto> GetCourseAsync(Guid id);
    Task<IEnumerable<CourseDto>> GetCoursesByTeacherAsync(Guid teacherId);
    Task<CourseDto> CreateCourseAsync(CreateCourseDto createCourseDto);
    Task UpdateCourseAsync(Guid id, UpdateCourseDto updateCourseDto);
    Task<IEnumerable<CourseDto>> SearchCourseByNameAsync(string query);
    Task<IEnumerable<CourseIdNameDto>> GetActiveCoursesExtendedAsync();
    Task<PagedList<CourseDto>> GetCoursesPagedAsync(CourseParameters parameters, bool trackChanges = false);
}
