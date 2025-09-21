namespace LMS.Shared.Dtos
{
    /// <summary>
    /// Data transfer object for a teacher (ApplicationUser) entity.
    /// </summary>
    public class TeacherDto
    {
        public string Id { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}
