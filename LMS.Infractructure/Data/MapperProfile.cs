using AutoMapper;
using Domain.Models.Entities;
using LMS.Shared.DTOs.AuthDtos;
using LMS.Shared.DTOs.CourseDTOs;
using LMS.Shared.DTOs.UserDTOs;

namespace LMS.Infractructure.Data;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserRegistrationDto, ApplicationUser>();
        CreateMap<ApplicationUser, StudentDto>();
        CreateMap<ApplicationUser, TeacherDto>();
        CreateMap<ApplicationUser, UserDto>();
        CreateMap<Course, CourseDto>();
    }
}
