using LMS.Shared.DTOs.CourseDTOs;
using LMS.Shared.DTOs.UserDTOs;
using LMS.Shared.DTOs.ModuleDTOs;

namespace Service.Contracts
{
    public interface IStudentService
    {
        Task<CourseWithTeachersDto?> GetCourseForStudentAsync(Guid studentId);
        Task<IEnumerable<ModuleDto>> GetModulesForStudentAsync(Guid studentId);
        Task<CourseWithModulesDto?> GetCourseWithModulesForStudentAsync(Guid studentId);
        Task<IEnumerable<StudentDto>> GetCoursematesAsync(Guid studentId);
    }
}