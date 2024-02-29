using Models.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.Users
{
    public class Teacher :Base
    {
        public Teacher()
        {
            Classes = new HashSet<Class>();
            Subjects = new HashSet<Subject>();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Class> Classes { get; set; }
        public ICollection<Subject> Subjects { get; set; }
      
    }
}
