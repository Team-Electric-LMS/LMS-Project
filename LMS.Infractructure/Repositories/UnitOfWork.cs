using Domain.Contracts.Repositories;
using LMS.Infractructure.Data;

namespace LMS.Infractructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext context;
    private readonly Lazy<IUserRepository> userRepository;
    private readonly Lazy<ICourseRepository> courseRepository;
    public IUserRepository UserRepository => userRepository.Value;
    public ICourseRepository CourseRepository => courseRepository.Value;

    public UnitOfWork(ApplicationDbContext context, Lazy<IUserRepository> userRepository, Lazy<ICourseRepository> courseRepository)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
    }
    public async Task CompleteAsync() => await context.SaveChangesAsync();
}
