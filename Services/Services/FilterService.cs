using Repository.IRepositories;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class FilterService : IFilterService
    {
        private readonly IFilterRepository _filterRepository;
        public FilterService(IFilterRepository filterRepository)
        {
            _filterRepository = filterRepository;
        }
        public async Task<List<int>> GetAcceses(string actionName, string controllerName, string roleName)
        {
            return await _filterRepository.GetAccesses(actionName, controllerName, roleName);
        }
    }
}
