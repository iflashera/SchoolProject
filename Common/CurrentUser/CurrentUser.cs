using Common.Security;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Common.CurrentUser
{
    public class CurrentUser :ICurrentUser
    {
        private static readonly Claim[] EmptyClaimsArray = new Claim[0];

        public virtual bool IsAuthenticated => Id > 0;

        public virtual int Id => this.FindClaimValue(CustomClaimTypes.Id) != null ? int.Parse(this.FindClaimValue(CustomClaimTypes.Id)) : _principalAccessor.HttpContext != null ? GetEmployeeIdFromSession() : 0;
        public virtual int UserId => this.FindClaimValue(CustomClaimTypes.UserId) != null ? int.Parse(this.FindClaimValue(CustomClaimTypes.UserId)) : _principalAccessor.HttpContext != null ? GetEmployeeIdFromSession() : 0;
        public virtual string FullName => this.FindClaimValue(CustomClaimTypes.Name) != null ? this.FindClaimValue(CustomClaimTypes.Name) : _principalAccessor.HttpContext != null ? Convert.ToString(_principalAccessor.HttpContext.Session.GetString("FullName")) : "";
        public virtual string RoleName => this.FindClaimValue(CustomClaimTypes.Role) != null ? this.FindClaimValue(CustomClaimTypes.Role) : _principalAccessor.HttpContext != null ? Convert.ToString(_principalAccessor.HttpContext.Session.GetString("RoleName")) : "";


        public virtual string[] Roles => FindClaims(CustomClaimTypes.Role) != null ? FindClaims(CustomClaimTypes.Role).Select(c => c.Value).ToArray() : new string[] { };


        private readonly IHttpContextAccessor _principalAccessor;

        public CurrentUser(IHttpContextAccessor principalAccessor)
        {
            _principalAccessor = principalAccessor;
        }

        public virtual Claim FindClaim(string claimType)
        {
            if (_principalAccessor.HttpContext != null && _principalAccessor.HttpContext.User != null)
            {
                try
                {
                    return _principalAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == claimType);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        public virtual Claim[] FindClaims(string claimType)
        {
            if (_principalAccessor.HttpContext != null && _principalAccessor.HttpContext.User != null)
            {
                try
                {
                    return _principalAccessor.HttpContext.User.Claims.Where(c => c.Type == claimType).ToArray() ?? EmptyClaimsArray;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        public virtual Claim[] GetAllClaims()
        {
            if (_principalAccessor.HttpContext != null && _principalAccessor.HttpContext.User != null)
                return _principalAccessor.HttpContext.User.Claims.ToArray() ?? EmptyClaimsArray;

            return null;
        }

        public virtual bool IsInRole(string roleName)
        {
            return FindClaims(CustomClaimTypes.Role).Any(c => c.Value == roleName);
        }

        private int GetEmployeeIdFromSession()
        {
            try
            {
                return Convert.ToInt32(_principalAccessor.HttpContext.Session.GetInt32("EmployeeId"));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
      

    }

}

