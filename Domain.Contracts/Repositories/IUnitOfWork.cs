namespace Domain.Contracts.Repositories;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    ICourseRepository Courses { get; }
    Task CompleteAsync();
}