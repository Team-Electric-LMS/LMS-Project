using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs.ActivityDTOs;
public class ActivityIdNameDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}
