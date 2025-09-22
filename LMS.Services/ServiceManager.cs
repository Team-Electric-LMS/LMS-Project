using Service.Contracts;

namespace LMS.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthService> authService;
    private readonly Lazy<ICourseService> courseService;

    public IAuthService AuthService => authService.Value;
    public ICourseService CourseService => courseService.Value;

    public ServiceManager(Lazy<IAuthService> authService, Lazy<ICourseService> courseService)
    {
        this.authService = authService;
        this.courseService = courseService;
    }
}
