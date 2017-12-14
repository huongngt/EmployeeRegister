using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeRegister.DataAccessLayer;
using EmployeeRegister.Models;

namespace EmployeeRegister.Controllers
{
    public class DepartmentsController : Controller
    {
        private RegisterContext db = new RegisterContext();

        // GET: Departments
        public ActionResult Index()
        {
            List<DepartmentsListViewModel> model= new List<DepartmentsListViewModel>();
            var departmentlist = db.Employees
                .GroupBy(e => e.Department)
                .Select(g => new {
                    dep = g.Key,
                    count = g.Count(),
                    salarysum = g.Sum( n => n.Salary)
                        })
                .ToList();

            foreach (var d in departmentlist)
            {
                model.Add(new DepartmentsListViewModel { DepartmentName = d.dep, NumberOfEmployees = d.count, SalarySum = d.salarysum });
            }
            return View(model);
        }

        // GET: Departments/Details/5
        public ActionResult Details(string name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employees = db.Employees.Where(e => e.Department == name).ToList();
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
