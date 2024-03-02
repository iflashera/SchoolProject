
using Common;
using Common.DTOs.Account;
using Common.Helper;
using Common.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Models.Models.Identity;
using Models.Models.Users;
using Newtonsoft.Json.Linq;
using Repository.IRepositories;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IRepository<UserDetail> _userRepository;
        private readonly IRepository<Admin> _adminRepository;
        private readonly IRepository<Parent> _parentRepository;
        private readonly IRepository<Teacher> _teacherRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountServices(
            IRepository<UserDetail> userRepository,
            IRepository<Admin> adminRepository,
            IRepository<Parent> parentRepository,
            IRepository<Teacher> teacherRepository,
            IRepository<Student> studentRepository,
            UserManager<ApplicationUser> userManager
            )
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _adminRepository = adminRepository;
            _parentRepository = parentRepository;
            _teacherRepository = teacherRepository;
        }
        public async Task<APIResponse<LoginViewModel>> Login(LoginDTO loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if(user== null)
            {
                return ResponseHelper<LoginViewModel>.CreateErrorRes(null, HttpStatusCode.Unauthorized, new List<string> { Constant.FailedLogin });
            }
            var userDetail = await _userRepository.FirstOrDefaultAsync(u => u.ApplicationUserId == user.Id);
            var userRole = await _userRepository.GetLoggedInUserRoleId(user);
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result)
            {
                return ResponseHelper<LoginViewModel>.CreateErrorRes(null, HttpStatusCode.Unauthorized, new List<string> { Constant.FailedLogin });
            }
            else
            {
                if (userDetail != null)
                {
                    if (userDetail.Role.RoleName == "Admin")
                    {
                        var admin = await _adminRepository.FirstOrDefaultAsync(i => i.UserId == userDetail.Id);
                        if (admin == null)
                            return null;
                        var token = jwtToken.GenerateJWTToken(_configuration, user, userRoles.RoleName, userDetail, admin.Id);
                        return GenerateLoginResponse(token, user, userDetail, Supadmin.Id);
                    }
                }
    }
}
