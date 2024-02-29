using Models.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.Classes
{
    public class Class :Base
    {
        public Class()
        {
            Subjects = new HashSet<Subject>();
            Teachers = new HashSet<Teacher>();
            Students= new HashSet<Student>();   
        }
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string ClassSection { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
        public ICollection<Student> Students { get; set; }
        public int SchoolId { get; set; }
    }
}

