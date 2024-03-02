using Common.DTOs.Account;
using Common.Helper;
using Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IAccountServices
    {
        Task<APIResponse<LoginViewModel>> Login(LoginDTO loginDto);
    }
}
