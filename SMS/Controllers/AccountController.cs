using Common.DTOs.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;


namespace API.Controllers
{
    [ApiController]
    [Route("/api/Account/[action]")]

    public class AccountController : BaseController
    {
        private readonly IAccountServices _accountServices;


        public AccountController(IAccountServices accountServices)
        {
            _accountServices = accountServices;

        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            return Response(await _accountServices.Login(loginDto));
        }

    }
}
