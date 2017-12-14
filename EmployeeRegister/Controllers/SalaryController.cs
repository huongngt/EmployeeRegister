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
    public class SalaryController : Controller
    {
        private RegisterContext db = new RegisterContext();

        // GET: Salary
        public ActionResult Index()
        {
            var model = new SalaryRangeViewModel();

            // bind the available values
            model.SalaryRange = ranges;

            return View(model);
        }



        public PartialViewResult GetSalaryRanges(string range)
        {
            var emp = db.Employees.ToList();
            if (range == "low")
                emp= db.Employees.Where(e => e.Salary < 20000).ToList();
            if (range == "medium")
                emp = db.Employees.Where(e => e.Salary >= 20000 && e.Salary < 40000).ToList();
            if (range == "hight")
                emp = db.Employees.Where(e => e.Salary >= 40000).ToList();


            return PartialView("EmployeesList", emp);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        static Dictionary<string, string> ranges = new Dictionary<string, string>()
        {
            {"low", "Below 20000kr"},
            {"medium", "From 20000kr to 40000kr"},
            {"hight", "Above 40000kr"},
        };
    }

    
}
