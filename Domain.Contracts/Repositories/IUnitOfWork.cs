namespace Domain.Contracts.Repositories;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    ICourseRepository Courses { get; }
    IStudentRepository Students { get; }
    Task CompleteAsync();
}