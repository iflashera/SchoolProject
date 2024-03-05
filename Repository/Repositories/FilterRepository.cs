using DataContext;
using Microsoft.EntityFrameworkCore;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class FilterRepository : IFilterRepository
    {
        private readonly SmsDbContext _dbContext;
        public FilterRepository(SmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<int>> GetAccesses(string actionName, string controllerName, string roleName)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
            return await (from a in _dbContext.ApplicationActions
                          join c in _dbContext.ApplicationControllers
                          on a.ApplicationControllerId equals c.Id
                          join ar in _dbContext.AccessInRoles on
                          a.Id equals ar.ApplicationActionId
                          where a.ActionName == actionName
                          && c.Name == controllerName
                          && ar.RoleId == role.Id
                          select ar.Id).ToListAsync();
        }
    }
}




