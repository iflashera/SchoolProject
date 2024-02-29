using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.Classes
{
    public class Grades :Base
    {
        public int Id { get; set; }
        public string Marks { get; set; }

        public int StudentId { get; set; }
        public int SubjectId { get; set; }


    }
}
