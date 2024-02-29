using Models.Models.Access;
using Models.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.Roles
{
    public class Role :Base
    {
        public Role()
        {
            AccessInRoles = new HashSet<AccessInRole>();
        }
        public int Id { get; set; }
        public string ApplicationRoleId { get; set; }
        public virtual ApplicationRole ApplicationRole { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<AccessInRole> AccessInRoles { get; set; }
    }
}
