using System.ComponentModel.DataAnnotations;

namespace LMS.Shared.DTOs.AuthDtos;
public record UserUpdateDto
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string UserName { get; set; } = string.Empty;

    //Optional if you want to add user to role when you register user
    //UI have to be updated to support this
    public string? Role { get; set; } = string.Empty;
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
}