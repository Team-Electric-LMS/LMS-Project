using LMS.Shared.DTOs.ActivityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs.ModuleDTOs;
public class ModuleIdNameDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public List<ActivityIdNameDto> Activities { get; set; } = new();
}