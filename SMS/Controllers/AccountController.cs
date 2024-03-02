using Common.DTOs.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            return Response(await _accountServices.Login(loginDto));
        }

    }
}
