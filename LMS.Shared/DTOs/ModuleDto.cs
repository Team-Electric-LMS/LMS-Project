using System;

namespace LMS.Shared.DTOs
{
    /// <summary>
    /// Data transfer object for Module entity.
    /// </summary>
    public class ModuleDto
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
