using AutoMapper;
using Domain.Models.Entities;
using LMS.Shared.DTOs.ActivityDTOs;
using LMS.Shared.DTOs.AuthDtos;
using LMS.Shared.DTOs.CourseDTOs;
using LMS.Shared.DTOs.UserDTOs;

namespace LMS.Infractructure.Data;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserRegistrationDto, ApplicationUser>();
        CreateMap<UserUpdateDto, ApplicationUser>();
        CreateMap<ApplicationUser, StudentDto>();
        CreateMap<ApplicationUser, TeacherDto>();
        CreateMap<ApplicationUser, UserDto>();
        CreateMap<ApplicationUser, UserUpdateDto>();
        CreateMap<ApplicationUser, UserRegistrationDto>();
        CreateMap<Course, CourseDto>();
        CreateMap<Activity, ActivityDto>();
        CreateMap<CreateCourseDto, Course>();
        CreateMap<UpdateCourseDto, Course>();
        CreateMap<CreateActivityDto, Activity>();
        CreateMap<UpdateActivityDto, Activity>();
    }
}
