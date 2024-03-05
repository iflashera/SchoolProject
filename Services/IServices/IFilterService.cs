using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IFilterService
    {
        Task<List<int>> GetAcceses(string actionName, string controllerName, string roleName);
    }
}
