namespace LMS.Shared.DTOs;
public class CourseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid TeacherId { get; set; }
}
