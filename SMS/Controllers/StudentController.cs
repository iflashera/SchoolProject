using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/Student/[action]")]
    public class StudentController :BaseController
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
                _studentService = studentService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetStudentsByClassId( int ClassId)
        {
            return Response(await _studentService.GetStudentsByClassId( ClassId));
        }

    }
}
