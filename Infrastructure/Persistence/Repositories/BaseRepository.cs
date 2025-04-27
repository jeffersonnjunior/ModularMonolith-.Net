using Common.IPersistence.IRepositories;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories;

public class BaseRepository : IBaseRepository
{
    private readonly AppDbContext _context;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> Query<T>() where T : class
    {
        return _context.Set<T>();
    }

    public T? Find<T>(Guid id) where T : class
    {
        return _context.Set<T>().Find(id);
    }

    public T Add<T>(T entity) where T : class
    {
        _context.Set<T>().Add(entity);
        return entity;
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
}
