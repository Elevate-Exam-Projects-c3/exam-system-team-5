using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using exam_system.Domain.Common;
using exam_system.Persistence.Context;
using Microsoft.EntityFrameworkCore.Query;
namespace exam_system.Persistence.DataAccess;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }

    public IQueryable<T> GetAll()
    {
        //add AsNoTracking
        return _dbSet.AsNoTracking().Where(e => !e.IsDeleted);
    }

    public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
    {
        //add AsNoTracking
        return GetAll().Where(predicate);
    }

    public async Task AddAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }


    // Soft Delete - marks as deleted but keeps in database
    public void Delete(T entity)
    {
        _dbSet.Attach(entity);
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;

        // Mark the entity as modified so EF will update it
        _context.Entry(entity).State = EntityState.Modified;
    }

    // Hard Delete - physically removes from database
    public void HardDelete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            Delete(entity); // Use soft delete for range as well
        }
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>>? criteria = null)
    {
        if (criteria == null)
        {
            return await _dbSet.CountAsync();
        }

        return await _dbSet.CountAsync(criteria);
    }

    public Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        Delete(entity); // Use soft delete
        return Task.CompletedTask;
    }

    public void Add(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        _dbSet.Add(entity);
    }

    //isExist
    public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return Get(predicate).AnyAsync(cancellationToken);
    }
    //soft Detate
    public Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return UpdateAsync(
            e => e.Id == id,
            setters => setters
                .SetProperty(e => e.IsDeleted, true)
                .SetProperty(e => e.DeletedAt, DateTime.UtcNow),
            cancellationToken);

    }
    //Update in Database
    public Task<int> UpdateAsync(
        Expression<Func<T, bool>> predicate,
        Action<UpdateSettersBuilder<T>> setters,
        CancellationToken cancellationToken = default)
    {
        return Get(predicate).ExecuteUpdateAsync(
            s =>
            {
                setters(s);                                        
                s.SetProperty(e => e.UpdatedAt, DateTime.UtcNow);  
            },
            cancellationToken);
    }

}

