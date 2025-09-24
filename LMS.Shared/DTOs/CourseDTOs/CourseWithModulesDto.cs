using System;
using System.Collections.Generic;
using LMS.Shared.DTOs.ModuleDTOs;

namespace LMS.Shared.DTOs.CourseDTOs;
public class CourseWithModulesDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public List<ModuleDto> Modules { get; set; } = new();
}
