namespace Domain.Contracts.Repositories;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    ICourseRepository Courses { get; }
    IStudentRepository Students { get; }
    IModuleRepository Modules { get; }
    IActivityRepository ActivityRepository { get; }
    IDocumentRepository DocumentRepository { get; }
    Task CompleteAsync();
}