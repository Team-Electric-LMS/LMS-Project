using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities;

public class Activity : EntityEdu
{
    public Guid ModuleId { get; set; }
    public Module Module { get; set; } = null!;
    public Guid ActivityTypeId { get; set; }
    public ActivityType ActivityType { get; set; } = null!;

    public ICollection<Document> Documents { get; set; } = new List<Document>();
    
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    // public string? Description { get; set; }
    // public DateOnly StartDate { get; set; }
    // public DateOnly EndDate { get; set; }

    public string Type { get; set; } = "Activity";

}