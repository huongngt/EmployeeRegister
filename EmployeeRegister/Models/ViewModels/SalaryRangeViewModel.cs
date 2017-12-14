using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeRegister.Models
{
    public class SalaryRangeViewModel
    {
        public string SelectedRange { get; set; }
        public Dictionary<string, string> SalaryRange { get; set; }
    }
}