using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.Users
{
    public class Parent :Base
    {
        public int Id { get; set; }
        public string ParentFirstName { get; set; }
        public string ParentLastName { get; set; }
        public int ParentUserId { get; set; }
        public string ParentUserName { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Student> Student { get; set; }
    }
}
