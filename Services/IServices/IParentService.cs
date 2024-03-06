using Common.DTOs.Parent;
using Common.DTOs.Teacher;
using Common.Helper;
using Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IParentService
    {
        Task<APIResponse<string>> AddParent(AddparentDto addParent);
        Task<APIResponse<List<ParentViewModel>>> GetAllParents();

        Task<APIResponse<string>> AddChildToParent(int parentId, List<AddChildDto> children);

    }
}
