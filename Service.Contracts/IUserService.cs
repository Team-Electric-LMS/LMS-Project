using LMS.Shared.DTOs;

namespace Service.Contracts;
public interface IUserService
{
    Task<bool> UserExistsAsync(string id);
    Task<UserDto> GetUserByIdAsync(string id, bool trackChanges = false);
}
