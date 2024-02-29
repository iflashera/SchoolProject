using Models.Models.Identity;
using Models.Models.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.Users
{
    public class UserDetail :Base
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public bool isActive { get; set; }
        public int SchoolId { get; set; }
    }
}
