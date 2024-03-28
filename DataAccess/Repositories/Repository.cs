using DataAccess.Contexts;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext _dbContext;

        public DbSet<T> Set { get; private set; }

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            var set = _dbContext.Set<T>();
            set.Load();
            Set = set;
        }

        public async Task CreateAsync(T item)
        {
            await Set.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T item)
        {
            Set.Remove(item);
            await _dbContext.SaveChangesAsync();

        }

        public async Task UpdateAsync(T item)
        {
            Set.Update(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await Set.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return Set;
        }

    }
}
