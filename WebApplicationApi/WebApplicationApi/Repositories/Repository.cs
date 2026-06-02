using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApplicationApi.Data;

namespace WebApplicationApi.Repositories;

public class Repository<T>:IRepository<T> where T : class
{
    private readonly TakssApiDbContext _context;
    private readonly DbSet<T> _dbSet;
    
    public Repository(TakssApiDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
    // Əlaqəli dataları gətirmək üçün
   public async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }
    
    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task SaveChangesAsync()
    { 
        await _context.SaveChangesAsync();
    }
}