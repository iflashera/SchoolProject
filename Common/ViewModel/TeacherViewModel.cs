using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModel
{
    public class TeacherViewModel
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public ICollection<ClassViewModel> Classes { get; set; }

    }
}
