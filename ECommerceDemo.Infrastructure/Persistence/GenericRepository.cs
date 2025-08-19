using ECommerceDemo.Application.Abstractions.Persistence;
using ECommerceDemo.Domain.Entities.Common.Base;
using Microsoft.EntityFrameworkCore;

namespace ECommerceDemo.Infrastructure.Persistence;

public class GenericRepository<T, TId>(ECommerceDemoDbContext dbContext) : IGenericRepository<T, TId> where T : BaseEntity<TId> where TId : struct
{
    protected ECommerceDemoDbContext context = dbContext;
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();

    public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);
    public void UpdateAsync(T entity) => _dbSet.Update(entity);
    public void DeleteAsync(T entity) => _dbSet.Remove(entity);
    public Task<List<T>> GetAllAsync() => _dbSet.ToListAsync();
    public ValueTask<T?> GetByIdAsync(TId id) => _dbSet.FindAsync(id);

}
