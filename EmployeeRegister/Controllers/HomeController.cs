using EmployeeRegister.DataAccessLayer;
using EmployeeRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace EmployeeRegister.Controllers
{
    public class HomeController : Controller
    {
        private RegisterContext db = new RegisterContext();
        public ActionResult Index(string name= "", string searchOption= "", string searchBy= "",string sortOption = "OrderBy", string sortBy = "FirstName")
        {
            var emp = new List<EmployeeRegister.Models.EmployeeViewModel>();

            IQueryable<EmployeeViewModel> queryableData = from e in db.Employees
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
            //build dynamic query
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(searchOption) && !string.IsNullOrEmpty(searchBy))
            {
                var queryResult = ExecuteDynamicQuery(name, searchOption, searchBy, queryableData);
                emp = ExecuteDynamicOrder(queryResult, sortOption, sortBy).ToList();
            }

            //var pageList = emp.ToPagedList(page,5);

            if (Request.IsAjaxRequest())
            {
                return PartialView("EmployeesVMList", emp);
            }
            return View(emp);
        }

        static IQueryable<EmployeeRegister.Models.EmployeeViewModel> ExecuteDynamicQuery(string searchValue, string searchOption, string searchBy, IQueryable<EmployeeViewModel> queryableData, string sortOption="OrderBy", string sortBy="FirstName")
        {
            var emp = new List<EmployeeRegister.Models.EmployeeViewModel>();
            Expression predicateBody;
            PropertyInfo propInfo = typeof(EmployeeViewModel).GetProperty(searchBy); //FirstName: property
            ParameterExpression pe = Expression.Parameter(typeof(EmployeeViewModel), searchBy); //{Firstname}
            Expression left = Expression.Property(pe, propInfo);//FirstName.FirstName
            Expression right = Expression.Constant(searchValue, propInfo.PropertyType);//"value"
            if (searchOption == "Exact")
            {
                predicateBody = Expression.Equal(left, right); //FirstName.FirstName = "value"
            }
            else
            {
                MethodInfo method = typeof(string).GetMethod(searchOption, new[] { typeof(string) });
                predicateBody = Expression.Call(left, method, right);
            }

          
            //// Create an expression tree that represents the expression            
            var queryableType = typeof(System.Linq.Queryable);            
            var whereClause = Expression.Lambda<Func<EmployeeViewModel, bool>>(predicateBody,
                              new ParameterExpression[] { pe }); //{FirstName => (FirstName.FirstName == "David")}


            var whereMethod = queryableType.GetMethods()
                   .First(m =>
                   {
                       var parameters = m.GetParameters().ToList();
                       //Put more restriction here to ensure selecting the right overload                
                       //the first overload that has 2 parameters
                       return m.Name == "Where" && m.IsGenericMethodDefinition &&
                                         parameters.Count == 2;
                   });


            var genericMethod = whereMethod.MakeGenericMethod(typeof(EmployeeViewModel));


            //execute query on queryData
            var newQuery = (IQueryable<EmployeeViewModel>)genericMethod
                          .Invoke(genericMethod, new object[] { queryableData, whereClause});

            return newQuery;
        }

        static IQueryable<EmployeeRegister.Models.EmployeeViewModel> ExecuteDynamicOrder(IQueryable<EmployeeViewModel> queryableData, string sortOption, string sortBy)
        {
            var emp = new List<EmployeeRegister.Models.EmployeeViewModel>();
            

            //// Create an expression tree that represents the expression            
            var queryableType = typeof(System.Linq.Queryable);
            PropertyInfo propInfo = typeof(EmployeeViewModel).GetProperty(sortBy);
            ParameterExpression pe = Expression.Parameter(typeof(EmployeeViewModel), sortBy);
            var propertyAccess = Expression.MakeMemberAccess(pe, propInfo);
            var orderClause = Expression.Lambda(propertyAccess, pe);


            var orderMethod = queryableType.GetMethods()
                   .First(m =>
                   {
                       var parameters = m.GetParameters().ToList();
                       //Put more restriction here to ensure selecting the right overload                
                       //the first overload that has 2 parameters
                       return m.Name == sortOption && m.IsGenericMethodDefinition
                       && parameters.Count == 2;
                   });

            var genericMethod = orderMethod.MakeGenericMethod(new[] { typeof(EmployeeViewModel), propertyAccess.Type });


            //execute query on queryData
            var newQuery = (IQueryable<EmployeeViewModel>)genericMethod
                          .Invoke(genericMethod, new object[] { queryableData, orderClause });

            return newQuery;
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