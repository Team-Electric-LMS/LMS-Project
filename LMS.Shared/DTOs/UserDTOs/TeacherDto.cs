using LMS.Shared.DTOs.CourseDTOs;

namespace LMS.Shared.DTOs.UserDTOs;
public class TeacherDto : UserDto
{
    public ICollection<CourseDto> CoursesTaught { get; set; } = new List<CourseDto>();

}