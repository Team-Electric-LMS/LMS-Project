using Domain.Contracts.Repositories;
using LMS.Infractructure.Data;

namespace LMS.Infractructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext context;
    private readonly Lazy<IUserRepository> userRepository;
    public IUserRepository UserRepository => userRepository.Value;

    public UnitOfWork(ApplicationDbContext context, Lazy<IUserRepository> userRepository)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
        this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }
    public async Task CompleteAsync() => await context.SaveChangesAsync();
}
