using Common;
using Common.CustomException;
using Common.DTOs.Parent;
using Common.Helper;
using Common.ViewModel;
using Repository.IRepositories;
using Repository.Repositories;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Models.Enum.Enums;

namespace Services.Services
{
    public class ParentService : IParentService
    {
        private readonly IParentRepository _parentRepository;
        public ParentService( IParentRepository parentRepository)
        {
            _parentRepository = parentRepository;   
        }

        public  async Task<APIResponse<string>> AddChildToParent(int parentId, List<AddChildDto> children)
        {
            return await _parentRepository.AddChildToParent(parentId, children);
        }

        public  async Task<APIResponse<string>> AddParent(AddparentDto addParent)
        {
            var res = await _parentRepository.AddParent(addParent);
            if (res == (int)AddUpdateResults.Success)
            {
                return ResponseHelper<string>.CreateSuccessRes(null, new List<string> { Constant.AddSuccess });
            }
            else if (res == (int)AddUpdateResults.Duplicate)
            {
                throw new CustomException("Parent or Child Email already exists");
            }
            else
            {
                return ResponseHelper<string>.CreateSuccessRes(null, HttpStatusCode.InternalServerError, new List<string> { "Internal server Error" });
            }
        }
        public async Task<APIResponse<List<ParentViewModel>>> GetAllParents()
        {
            return await _parentRepository.GetAllParents();
        }
    }
}
