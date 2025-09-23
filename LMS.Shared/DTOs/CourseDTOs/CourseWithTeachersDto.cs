using LMS.Shared.DTOs.CourseDTOs;
using LMS.Shared.DTOs.UserDTOs;

namespace LMS.Shared.DTOs.CourseDTOs;
public class CourseWithTeachersDto
{
    public CourseDto Course { get; set; } = null!;
    public List<LMS.Shared.DTOs.UserDTOs.TeacherDto> Teachers { get; set; } = new();
}
