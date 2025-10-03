namespace LMS.Shared.DTOs.DocumentDTOs;
public class DocumentUploadDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string UploadedById { get; set; } = null!;
    public Guid? CourseId { get; set; }
    public Guid? ModuleId { get; set; }
    public Guid? ActivityId { get; set; }
}