using LMS.Shared.DTOs.AuthDtos;
using LMS.Shared.DTOs.UserDTOs;
using System.Threading.Tasks;

namespace Service.Contracts;
public interface IUserService
{
    Task<bool> UserExistsAsync(string id);
    Task<bool> EmailExistsAsync(string id);
    Task<bool> UserNameExistsAsync(string id);

    Task<UserDto> GetUserByIdAsync(string id, bool includeCourse = true, bool trackChanges = false);
    Task<UserUpdateDto> GetUserByIdentityName(string name, bool includeCourse = true, bool trackChanges = false);
    Task UpdateUserCourseAsync(string id, bool unassign, AddUserCourseByIdDto? dto, bool trackChanges = true);
    Task<IEnumerable<UserDto>> GetStudents(Guid? id);
}
