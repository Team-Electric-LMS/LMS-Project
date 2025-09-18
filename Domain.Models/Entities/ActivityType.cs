using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities;
public class ActivityType : Entity
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    public ICollection<Activity> Activities { get; set; } = new List<Activity>();
}
