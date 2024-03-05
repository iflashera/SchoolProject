using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Security
{
    public class CustomClaimTypes
    {
        public static string UserName { get; set; } = System.Security.Claims.ClaimTypes.Name;

        public static string Name { get; set; }= System.Security.Claims.ClaimTypes.Name;
        public static string Role { get; set; }= System.Security.Claims.ClaimTypes.Role;
        public static string UserId { get; set; } = System.Security.Claims.ClaimTypes.NameIdentifier;
        public static string Id { get; set; } = System.Security.Claims.ClaimTypes.Sid;
        public static string Sid { get; set; } = System.Security.Claims.ClaimTypes.Sid;
    }
}
