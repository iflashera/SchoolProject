using Common.DTOs.Parent;
using Common.DTOs.Teacher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/Parent/[action]")]
    public class ParentController :BaseController
    {
        private readonly IParentService _parentService;  
        public ParentController( IParentService parentService)
        {
            _parentService = parentService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateParent(AddparentDto addParentDto)
        {
            return Response(await _parentService.AddParent(addParentDto));
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllParents()
        {
            return Response(await _parentService.GetAllParents());
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddChildToParent(int parentId, List<AddChildDto> childDto)
        {
            return Response(await _parentService.AddChildToParent(parentId, childDto));
        
        }
    }
}
