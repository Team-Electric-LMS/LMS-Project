using LMS.Shared.DTOs.AuthDtos;
using LMS.Shared.DTOs.UserDTOs;
using System.Threading.Tasks;

namespace Service.Contracts;
public interface IUserService
{
    Task<bool> UserExistsAsync(string id);
    Task<bool> EmailExistsAsync(string id);
    Task<bool> UserNameExistsAsync(string id);

    Task<UserDto> GetUserByIdAsync(string id, bool trackChanges = false);
    Task<UserDto> GetUserByIdentityName(string name, bool trackChanges = false);
    Task<UserDto> GetUserWithCourse(string? id, string? email, bool trackChanges = false);
    Task UpdateUserCourseAsync(string id, bool unassign, AddUserCourseByIdDto? dto, bool trackChanges = true);
    Task<IEnumerable<UserDto>> GetStudents(Guid? id);
}
