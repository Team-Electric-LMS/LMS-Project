using Domain.Contracts.Repositories;
using LMS.Infractructure.Data;

namespace LMS.Infractructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext context;
    private readonly Lazy<IUserRepository> userRepository;
    private readonly Lazy<ICourseRepository> courseRepository;
    private readonly Lazy<IStudentRepository> studentRepository;
    private readonly Lazy<IModuleRepository> moduleRepository;

    public IUserRepository UserRepository => userRepository.Value;
    public ICourseRepository Courses => courseRepository.Value;
    public IStudentRepository Students => studentRepository.Value;
    public IModuleRepository Modules => moduleRepository.Value;

    public UnitOfWork(ApplicationDbContext context, Lazy<IUserRepository> userRepository, Lazy<ICourseRepository> courseRepository,
        Lazy<IStudentRepository> studentRepository)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
        this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        this.courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
        this.studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
        this.moduleRepository = moduleRepository ?? throw new ArgumentNullException(nameof(moduleRepository));
    }
    public async Task CompleteAsync() => await context.SaveChangesAsync();
}
