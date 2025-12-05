namespace RickAndMorty.Application.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
