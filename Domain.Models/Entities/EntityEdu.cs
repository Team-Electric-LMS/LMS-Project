using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities;

public abstract class EntityEdu : Entity
{
[Required]
[MaxLength(50)]
public string? Name { get; set; } = null!;

[Required]
[MaxLength(800)]
public string? Description { get; set; } = null!;
public DateOnly StartDate { get; set; }
public DateOnly EndDate { get; set; }
}
