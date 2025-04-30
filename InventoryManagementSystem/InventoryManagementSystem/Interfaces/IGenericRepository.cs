using System.Linq.Expressions;

namespace InventoryManagementSystem.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task InsertAsync(T obj);
        void Update(T obj);
        Task DeleteAsync(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(int id);
        Task SaveAsync();
    }
}
