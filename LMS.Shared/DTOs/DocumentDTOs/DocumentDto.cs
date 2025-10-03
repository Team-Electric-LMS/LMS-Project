using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs.DocumentDTOs;
public class DocumentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Link { get; set; } = null!;
    public DateOnly UploadDate { get; set; }
    public string UploadedById { get; set; } = null!;
    public Guid? CourseId { get; set; }
    public Guid? ModuleId { get; set; }
    public Guid? ActivityId { get; set; }
}
