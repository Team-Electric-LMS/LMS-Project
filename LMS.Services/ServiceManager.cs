using Service.Contracts;

namespace LMS.Services;

public class ServiceManager : IServiceManager
{
    private Lazy<IAuthService> authService;
    private Lazy<IUserService> userService;
    public IAuthService AuthService => authService.Value;
    public IUserService UserService => userService.Value;

    public ServiceManager(Lazy<IAuthService> authService, Lazy<IUserService> userService)
    {
        this.authService = authService;
        this.userService = userService;
    }
}
