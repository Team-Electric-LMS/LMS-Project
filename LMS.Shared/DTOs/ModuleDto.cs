using System;

namespace LMS.Shared.Dtos
{
    /// <summary>
    /// Data transfer object for a module entity.
    /// </summary>
    public class ModuleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Guid CourseId { get; set; }
    }
}
