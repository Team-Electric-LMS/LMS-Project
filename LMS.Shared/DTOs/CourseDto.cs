namespace LMS.Shared.DTOs
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string? CourseName { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TeacherName { get; set; } = string.Empty;
    }

}
