namespace Domain.Contracts.Repositories;
public interface IRepositoryBase<T>
{
    Task<T?> GetEntityByIdAsync(Guid id, bool trackChanges = false);
    Task<bool> EntityExistsAsync(Guid id);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);

}
