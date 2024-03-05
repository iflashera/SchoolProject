using Common.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Models.Identity;
using Models.Models.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class JWTToken
    {
        public JwtSecurityToken GenerateJWTToken(IConfiguration configuration, ApplicationUser user, string userRolesName, UserDetail userDetail, int Id)
        {
            var authClaims = new List<Claim>
            {
                    new Claim(CustomClaimTypes.Id, Convert.ToString(Id)),
                    new Claim(CustomClaimTypes.UserId, Convert.ToString(userDetail.Id)),                   
                    new Claim(CustomClaimTypes.Name,user.UserName),
                    new Claim(CustomClaimTypes.Role,Convert.ToString(userRolesName)),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            DateTime expiryDate;
            if (userRolesName == "Admin")
            {
                expiryDate = DateTime.Now.AddDays(1);
            }
            else
            {
                expiryDate = DateTime.Now.AddDays(1);
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
               issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: expiryDate,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;

        }
         
    }
}
