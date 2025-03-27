using Microsoft.EntityFrameworkCore;

namespace Common.Data;

public interface IBaseRepository
{
    IQueryable<T> Query<T>() where T : class;
    T? Find<T>(Guid id) where T : class;
    void Add<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    void Remove<T>(T entity) where T : class;
    void SaveChanges();
}