namespace DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(T item);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
    }
}
