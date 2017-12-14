using EmployeeRegister.DataAccessLayer;
using EmployeeRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeRegister.Controllers
{
    public class ContactController : Controller
    {
        private RegisterContext db = new RegisterContext();
        // GET: Contact
        public ActionResult Index(string name = null)
        {
            if (name !=null)
            {
                var employeeVM = from e in db.Employees
                                     //where e.FirstName
                                 select new EmployeeViewModel
                                 {
                                     FirstName = e.FirstName,
                                     LastName = e.LastName,
                                     FullName = e.FirstName + " " + e.LastName,
                                     Salary = e.Salary,
                                     Department = e.Department,
                                     Position = e.Position,
                                     EmployeeCode = e.EmployeeCode,
                                 };

                var employee = employeeVM.FirstOrDefault(e => e.FullName == name);
                if (employee == null)
                {
                    return HttpNotFound();
                }
                return View(employee);
            }
            
            
            return View();
        }
    }
}