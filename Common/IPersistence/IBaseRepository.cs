using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Common.IPersistence;

public interface IBaseRepository
{
    IQueryable<T> Query<T>() where T : class;
    T? Find<T>(Guid id) where T : class;
    void Add<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    void Remove<T>(T entity) where T : class;
    void SaveChanges();
    IQueryable<T> QueryWithIncludes<T>(params Expression<Func<T, object>>[] includes) where T : class;
    Task<T?> FindAsync<T>(Guid id) where T : class;
    Task SaveChangesAsync();
}