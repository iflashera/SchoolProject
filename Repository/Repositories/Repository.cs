using DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Models.Identity;
using Models.Models.Roles;
using Repository.IRepositories;
using System.Linq.Expressions;

namespace Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : Base
    {
        private readonly SmsDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUser _currentUser;
        public virtual DbSet<T> DbSet => _dbContext.Set<T>();

        public Repository(
            SmsDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _currentUser = currentUser;
        }
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<Role> GetLoggedInUserRoleId(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var roleName = roles.FirstOrDefault();
            var roleDetail = _dbContext.Roles.FirstOrDefault(r => r.RoleName == roleName);
            return roleDetail;
        }
        public async Task<T> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> Create(T entity)
        {
           
            var savedEntity = _dbContext.Add(entity).Entity;
            _dbContext.SaveChanges();
            return savedEntity;
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            _dbContext.SaveChanges();
        }

        public async Task<int> Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            _dbContext.SaveChanges();
        }

        public async Task<T> Update(T entity)
        {
            var savedEntity = _dbContext.Update(entity).Entity;
            _dbContext.SaveChanges();
            return savedEntity;
        }

        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }

       
    }
}
