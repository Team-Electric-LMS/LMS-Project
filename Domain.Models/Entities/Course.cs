using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities;
public class Course: EntityEdu
{
    public ICollection<Module> Modules { get; set; } = new List<Module>();
    public ICollection<Document> Documents { get; set; } = new List<Document>();
    public ICollection<ApplicationUser> Students { get; set; } = new List<ApplicationUser>();
    public ICollection<ApplicationUser> Teachers { get; set; } = new List<ApplicationUser>();

}
