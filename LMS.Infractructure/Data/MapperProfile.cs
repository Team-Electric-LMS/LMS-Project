using AutoMapper;
using Domain.Models.Entities;
using LMS.Shared.DTOs.ActivityDTOs;
using LMS.Shared.DTOs.AuthDtos;
using LMS.Shared.DTOs.CourseDTOs;
using LMS.Shared.DTOs.DocumentDTOs;
using LMS.Shared.DTOs.ModuleDTOs;
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
        CreateMap<DocumentUploadDto, Document>()
            .ForMember(d => d.Link, opt => opt.Ignore())
            .ForMember(d => d.UploadDate, opt => opt.Ignore()); ;
        CreateMap<Document, DocumentDto>().ReverseMap();

        CreateMap<Activity, ActivityIdNameDto>();
        CreateMap<Module, ModuleIdNameDto>();
        CreateMap<Course, CourseIdNameDto>();


        // CreateMap<Activity, ActivityDto>()
        //     .ForMember(d => d.ActivityTitle,    o => o.MapFrom(s => s.Name))
        //     .ForMember(d => d.Description,      o => o.MapFrom(s => s.Description))
        //     .ForMember(d => d.StartDate,        o => o.MapFrom(s => s.StartDate))
        //     .ForMember(d => d.EndDate,          o => o.MapFrom(s => s.EndDate))
        //     .ForMember(d => d.ActivityTypeName, o => o.MapFrom(s => s.ActivityType.Name));

    }
}
