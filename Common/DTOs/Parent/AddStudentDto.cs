﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs.Parent
{
    public class AddStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RollNo { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int ParentId { get; set; }
        public int ClassId { get; set; }
    }
}
