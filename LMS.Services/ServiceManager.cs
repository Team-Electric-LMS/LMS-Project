using Service.Contracts;

namespace LMS.Services;

public class ServiceManager : IServiceManager
{
    private Lazy<IAuthService> authService;
    private Lazy<IStudentService> studentService;
    public IAuthService AuthService => authService.Value;
    public IStudentService StudentService => studentService.Value;

    public ServiceManager(
        Lazy<IAuthService> authService,
        Lazy<IStudentService> studentService
    )
    {
        this.authService = authService;
        this.studentService = studentService;
    }
}
