using LMS.Shared.DTOs.CourseDTOs;

namespace LMS.Shared.DTOs.UserDTOs;
public class StudentDto : UserDto
{
    public CourseDto? Course { get; set; }

}