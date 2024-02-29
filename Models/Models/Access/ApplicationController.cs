using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.Access
{
    public class ApplicationController : Base 
    {
        public ApplicationController()
        {
            ApplicationActions = new HashSet<ApplicationAction>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ApplicationAction> ApplicationActions { get; set; }

    }
}
