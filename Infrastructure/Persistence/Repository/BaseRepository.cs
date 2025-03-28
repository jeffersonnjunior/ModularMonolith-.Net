using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Common.IPersistence;

namespace Infrastructure.Persistence.Repository;

public class BaseRepository : IBaseRepository
{
    protected readonly AppDbContext _context;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> Query<T>() where T : class
    {
        return _context.Set<T>().AsQueryable();
    }

    public IQueryable<T> QueryWithIncludes<T>(params Expression<Func<T, object>>[] includes) where T : class
    {
        var query = _context.Set<T>().AsQueryable();
        
        if (includes != null)
        {
            query = includes.Aggregate(query, 
                (current, include) => current.Include(include));
        }
        
        return query;
    }

    public T? Find<T>(Guid id) where T : class
    {
        return _context.Set<T>().Find(id);
    }

    public async Task<T?> FindAsync<T>(Guid id) where T : class
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public void Add<T>(T entity) where T : class
    {
        _context.Set<T>().Add(entity);
    }

    public void Update<T>(T entity) where T : class
    {
        _context.Set<T>().Update(entity);
    }

    public void Remove<T>(T entity) where T : class
    {
        _context.Set<T>().Remove(entity);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}