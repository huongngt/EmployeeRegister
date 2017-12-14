﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeRegister.Models
{
    public class EmployeeViewModel
    {
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int Salary { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
    }
}