using System.Linq.Expressions;

namespace NextAuto.Domain.Entities;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<HashSet<T>> GetAllAsync();
    Task<HashSet<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task<int> SaveChangesAsync();
}