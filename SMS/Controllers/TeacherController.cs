using Common.DTOs.Teacher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/Teacher/[action]")]
    public class TeacherController :BaseController
    {
        private readonly ITeacherService _teacherService;
        public TeacherController(ITeacherService teacherServices)
        {
            _teacherService = teacherServices;
        }
        [AllowAnonymous]
        [HttpPost]   
        public async Task<IActionResult> AddTeacher(AddTeacherDto teacherDto)
        {
            return Response(await _teacherService.CreateTeacher(teacherDto));
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateClass(AddClassDto addClassDto)
        {
            return Response(await _teacherService.AddClass(addClassDto));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateSubject(AddSubjectDto addSubjectDto)
        {
            return Response(await _teacherService.AddSubject(addSubjectDto));
        }
    }
}
