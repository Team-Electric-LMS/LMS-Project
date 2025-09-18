using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Models.Entities;
public class Document : Entity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Link { get; set; } = null!;
    public DateOnly UploadDate { get; set; }

    public string UploadedById { get; set; } = null!;
    public ApplicationUser UploadedBy { get; set; } = null!;

    public Guid? DocumentTypeId { get; set; }
    public DocumentType? DocumentType { get; set; } = null!;

    public Guid? CourseId { get; set; }
    public Course? Course { get; set; } = null!;

    public Guid? ModuleId { get; set; }
    public Module? Module { get; set; } = null!;

    public Guid? ActivityId { get; set; }
    public Activity? Activity { get; set; } = null!;
}