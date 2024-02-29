using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.Access
{
    public class ApplicationAction: Base
    {
        public ApplicationAction()
        {
            AccessInRoles = new HashSet<AccessInRole>();
        }
        public int Id { get; set; }
        public int ApplicationControllerId { get; set; }
        public virtual ApplicationController ApplicationController { get; set; }
        public string ActionName { get; set; }
        public string AccessDescription { get; set; }
        public virtual ICollection<AccessInRole> AccessInRoles { get; set; }
    }
}
