using LMS.Shared.DTOs.ModuleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs.CourseDTOs;
public class CourseIdNameDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public List<ModuleIdNameDto> Modules { get; set; } = new();
}