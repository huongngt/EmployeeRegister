using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeRegister.Models
{
    public class DepartmentsListViewModel
    {
        public string DepartmentName { get; set; }
        public int NumberOfEmployees { get; set; }
        public int SalarySum { get; set; }
    }
}