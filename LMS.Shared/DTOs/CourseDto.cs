using System;

namespace LMS.Shared.Dtos
{
    /// <summary>
    /// Data transfer object for a course entity.
    /// </summary>
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
