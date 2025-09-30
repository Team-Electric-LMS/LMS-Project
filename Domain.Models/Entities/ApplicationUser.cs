using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Entities;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpireTime { get; set; }
    public string? FirstName { get; set; } = null!;
    public string? LastName { get; set; } = null!;
    public Guid? CourseId { get; set; }
    public Course? Course { get; set; }
    public ICollection<Course> CoursesTaught { get; set; } = new List<Course>();
}