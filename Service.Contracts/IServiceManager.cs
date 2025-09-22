namespace Service.Contracts;
public interface IServiceManager
{
    IAuthService AuthService { get; }
    IUserService UserService { get; }
    ICourseService CourseService { get; }
}