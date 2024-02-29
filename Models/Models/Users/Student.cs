using Models.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.Users
{
    public class Student :Base
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RollNo { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int ParentId { get; set; }
        public Parent Parent { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public bool IsActive { get; set; } = true;




    }
}
