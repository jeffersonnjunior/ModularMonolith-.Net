namespace Common.IPersistence.IRepositories;

public interface IBaseRepository
{
    IQueryable<T> Query<T>() where T : class;
    T? Find<T>(Guid id) where T : class;
    T Add<T>(T entity) where T : class; 
    void Update<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
    void SaveChanges();
}