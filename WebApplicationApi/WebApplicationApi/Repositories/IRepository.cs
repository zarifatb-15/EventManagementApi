using System.Linq.Expressions;
using WebApplicationApi.Entity;

namespace WebApplicationApi.Repositories;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    // Əlaqəli dataları gətirmək üçün
    Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveChangesAsync();
}