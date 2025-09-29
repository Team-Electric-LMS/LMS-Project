namespace LMS.Shared.DTOs.CourseDTOs;

public record CreateCourseDto(
    string Name,
    string Description,
    DateOnly StartDate,
    DateOnly EndDate
);