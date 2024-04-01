using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs.Teacher
{
    public class UpdateTeacherDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<int> ClassIds { get; set; }
         public ICollection<int> SubjectIds { get; set; }
    }
}
