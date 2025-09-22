using LMS.Shared.DTOs.UserDTOs;

namespace Service.Contracts;
public interface IUserService
{
    Task<bool> UserExistsAsync(string id);
    Task<UserDto> GetUserByIdAsync(string id, bool includeCourse = true, bool trackChanges = false);
    Task UpdateUserCourseAsync(string id, bool unassign, AddUserCourseByIdDto? dto, bool trackChanges = true);
    Task<IEnumerable<UserDto>> GetStudents(Guid? id);
}
