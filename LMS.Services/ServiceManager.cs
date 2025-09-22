using Service.Contracts;

namespace LMS.Services;

public class ServiceManager : IServiceManager
{
    private Lazy<IAuthService> authService;
    private Lazy<IUserService> userService;
    private Lazy<ICourseService> courseService;

    public IAuthService AuthService => authService.Value;
    public IUserService UserService => userService.Value;
    public ICourseService CourseService => courseService.Value;

    public ServiceManager(Lazy<IAuthService> authService, Lazy<IUserService> userService, Lazy<ICourseService> courseService)
    {
        this.authService = authService;
        this.userService = userService;
        this.courseService = courseService;
    }
}