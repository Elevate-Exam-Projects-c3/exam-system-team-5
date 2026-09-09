using exam_system.Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace exam_system.Persistence.DataAccess;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);
    IQueryable<T> GetAll();
    IQueryable<T> Get(Expression<Func<T, bool>> predicate);

    void Add(T entity);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
    Task UpdateAsync(T entity);
    void Delete(T entity);
    Task DeleteAsync(T entity);
    void HardDelete(T entity);
    void DeleteRange(IEnumerable<T> entities);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<int> CountAsync(Expression<Func<T, bool>>? criteria = null);

    Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<int> UpdateAsync(Expression<Func<T, bool>> predicate,
    Action<UpdateSettersBuilder<T>> setters,
    CancellationToken cancellationToken);
}
