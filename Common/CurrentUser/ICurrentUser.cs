using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Common.CurrentUser
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }
        int Id { get; }
        int UserId { get; }
        string FullName { get; }
        string RoleName { get; }
        string[] Roles { get; }
        Claim FindClaim(string claimType);
        Claim[] FindClaims(string claimType);
        Claim[] GetAllClaims();
        bool IsInRole(string roleName);
    }
}
