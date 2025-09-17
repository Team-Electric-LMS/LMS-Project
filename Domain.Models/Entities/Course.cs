using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities;
public class Course: Entity
{
    public string? Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public ICollection<Module> Modules { get; set; } = new List<Module>();
    public ICollection<Document> Documents { get; set; } = new List<Document>();

}
