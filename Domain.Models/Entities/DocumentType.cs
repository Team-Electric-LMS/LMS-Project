using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities;
public class DocumentType : Entity
{
    public string Name { get; set; } = null!;
    public ICollection<Document> Documents { get; set; } = new List<Document>();
}
