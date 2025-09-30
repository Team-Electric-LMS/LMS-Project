using LMS.Shared.DTOs.CourseDTOs;
using Service.Contracts;
using Domain.Contracts;
using Domain.Contracts.Repositories;

namespace LMS.Services;

public class ServiceManager : IServiceManager
{
    private Lazy<IAuthService> authService;
    private Lazy<IUserService> userService;
    private Lazy<ICourseService> courseService;
    private Lazy<IStudentService> studentService;
    public IAuthService AuthService => authService.Value;
    public IUserService UserService => userService.Value;
    public ICourseService CourseService => courseService.Value;
    public IStudentService StudentService => studentService.Value;

    public IModuleService ModuleService { get; }

    public ServiceManager(Lazy<IAuthService> authService, Lazy<IUserService> userService, Lazy<ICourseService> courseService, Lazy<IStudentService> studentService)
    {
        this.authService = authService;
        this.userService = userService;
        this.courseService = courseService;
        this.studentService = studentService;
    }
}