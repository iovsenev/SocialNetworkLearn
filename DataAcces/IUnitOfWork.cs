
namespace DataAccess
{
    public interface IUnitOfWork<TEntity> : IDisposable
    {
        int SaveChanges(bool ensureAutoHistory = false);

        IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true)
            where TEntity : class;
    }
}
