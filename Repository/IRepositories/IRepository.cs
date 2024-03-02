using Models.Models.Identity;
using Models.Models.Roles;
using System.Linq.Expressions;

namespace Repository.IRepositories
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<Role> GetLoggedInUserRoleId(ApplicationUser user);
        Task<TEntity> GetAsync(int id);
        Task<TEntity> Create(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<int> Remove(TEntity entity);
        Task UpdateRange(IEnumerable<TEntity> entities);
        Task<int> Complete();
    }
}
