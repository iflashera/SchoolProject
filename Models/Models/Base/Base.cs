using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Base
    {
        public DateTime CreatedDate { get; set; }
        public DateTime TimeStamp { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
        public virtual int? CreatedBy { get; set; }
        public virtual int? UpdatedBy { get; set; }
    }
}
