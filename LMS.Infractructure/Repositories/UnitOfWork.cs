using Domain.Contracts.Repositories;
using LMS.Infractructure.Data;

namespace LMS.Infractructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext context;
    public ICourseRepository Courses { get; }

    public UnitOfWork(ApplicationDbContext context, ICourseRepository courseRepository)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
        Courses = courseRepository;
    }

    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();

    public async Task CompleteAsync() => await context.SaveChangesAsync();
}
