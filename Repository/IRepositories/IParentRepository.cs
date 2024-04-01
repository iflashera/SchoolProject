using Common.CurrentUser;
using Common.DTOs.Parent;
using Common.Helper;
using Common.ViewModel;
using DataContext;
using Microsoft.AspNetCore.Identity;
using Models.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IParentRepository
    {
        Task<int> AddParent(AddparentDto parentDTO);
        Task<APIResponse<List<ParentViewModel>>> GetAllParents();

        Task<APIResponse<string>> AddChildToParent(int parentId, List<AddChildDto> children);
        Task<APIResponse<string>> UpdateParent( UpdateParentDto ParentDto);

    }
}

