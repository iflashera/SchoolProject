using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IFilterRepository
    {
        Task<List<int>> GetAccesses(string actionName, string controllerName, string roleName);
    }
}
