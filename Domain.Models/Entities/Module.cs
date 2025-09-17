using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities;
public class Module : Entity
{
    public string? Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    public ICollection<Document> Documents { get; set; } = new List<Document>();
    public Course Course { get; set; } = null!;
    public Guid CourseId { get; set; }

}
