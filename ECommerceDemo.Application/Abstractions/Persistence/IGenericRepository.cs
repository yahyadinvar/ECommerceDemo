namespace ECommerceDemo.Application.Abstractions.Persistence;

public interface IGenericRepository<T, TId> where T : class where TId : struct
{
    Task<List<T>> GetAllAsync();
    ValueTask<T?> GetByIdAsync(TId id);
    ValueTask AddAsync(T entity);
    void UpdateAsync(T entity);
    void DeleteAsync(T entity);
}
