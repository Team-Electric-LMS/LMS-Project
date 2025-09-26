using LMS.Shared.DTOs.CourseDTOs;

namespace LMS.Shared.DTOs.UserDTOs;
public class UserDto
{
    public string Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Role { get; set; } = null!;
}