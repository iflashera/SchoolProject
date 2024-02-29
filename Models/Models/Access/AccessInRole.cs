using Models.Models.Roles;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.Access
{
    public class AccessInRole:Base
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public int ApplicationActionId { get; set; }
        public virtual ApplicationAction ApplicationAction { get; set; }
    }
}
