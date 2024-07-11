using ShortenUrl.Domain;

namespace ShortenUrl.Persistence.Repository.Contracts;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<bool> AddAsync(T entity);
    Task<bool> RemoveByIdAsync(int id);
}