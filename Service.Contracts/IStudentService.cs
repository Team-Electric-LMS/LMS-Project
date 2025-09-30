using LMS.Shared.DTOs.ActivityDTOs;
using LMS.Shared.DTOs.CourseDTOs;
using LMS.Shared.DTOs.ModuleDTOs;
using LMS.Shared.DTOs.UserDTOs;
using System.Reflection;

namespace Service.Contracts
{
    public interface IStudentService
    {
        Task<CourseWithTeachersDto?> GetCourseForStudentAsync(Guid studentId);
        Task<IEnumerable<ModuleDto>> GetModulesForStudentAsync(Guid studentId);
        Task<CourseWithModulesDto?> GetCourseWithModulesForStudentAsync(Guid studentId);
        Task<IEnumerable<ActivityDto>> GetActivitiesForModuleAsync(Guid studentId, Guid moduleId);
        Task<IEnumerable<StudentDto>> GetCoursematesAsync(Guid studentId);
    }
}