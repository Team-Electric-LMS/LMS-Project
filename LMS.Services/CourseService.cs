using LMS.Shared.DTOs;
using Service.Contracts;

namespace LMS.Services;

public class CourseService : ICourseService
{
    public Task<IEnumerable<CourseDto>> GetCoursesByTeacherAsync(Guid teacherId)
    {
        // Mock data for demonstration
        // TODO: Once the entity is created, this should fetch data from the database
        var courses = new List<CourseDto>
        {
            new CourseDto { Id = Guid.NewGuid(), Name = "Math", TeacherId = teacherId },
            new CourseDto { Id = Guid.NewGuid(), Name = "Physics", TeacherId = teacherId }
        };
        return Task.FromResult<IEnumerable<CourseDto>>(courses);
    }
}
