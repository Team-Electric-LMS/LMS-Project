using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using LMS.Infractructure.Data;
using LMS.Shared.Parameters;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LMS.Infractructure.Repositories;
public abstract class RepositoryBase<T> : IRepositoryBase<T>, IInternalRepositoryBase<T> where T : Entity
{
    protected DbSet<T> DbSet { get; }

    public RepositoryBase(ApplicationDbContext context)
    {
        DbSet = context.Set<T>();
    }

    public IQueryable<T> FindAll(bool trackChanges = false) =>
        !trackChanges ? DbSet.AsNoTracking() :
                        DbSet;

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
        !trackChanges ? DbSet.Where(expression).AsNoTracking() :
                        DbSet.Where(expression);

    public void Create(T entity) => DbSet.Add(entity);

    public void Update(T entity) => DbSet.Update(entity);

    public void Delete(T entity) => DbSet.Remove(entity);

    public async Task<bool> EntityExistsAsync(Guid id) => await DbSet.AnyAsync(m => m.Id == id);
    public async Task<T?> GetEntityByIdAsync(Guid id, bool trackChanges = false)
            => await FindByCondition(e => e.Id == id, trackChanges).FirstOrDefaultAsync();

    public async Task<PagedList<T>> GetPagedAsync(EntityParameters parameters, bool trackChanges = false)
           => await PagedList<T>.PageAsync(FindAll(trackChanges), parameters.PageNumber, parameters.PageSize);
}
