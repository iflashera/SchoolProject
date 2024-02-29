using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.Identity
{
    public class ApplicationUser :Base
    {
        public string FirsName { get; set; }
        public string LastName { get; set; }
    }
}
