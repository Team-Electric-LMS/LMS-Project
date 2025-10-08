namespace LMS.Shared.DTOs.ActivityDTOs;

public class UpdateActivityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string ActivityTypeName { get; set; } = null!;
}