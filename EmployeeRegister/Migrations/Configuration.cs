namespace EmployeeRegister.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EmployeeRegister.DataAccessLayer.RegisterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;

        }

        protected override void Seed(EmployeeRegister.DataAccessLayer.RegisterContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Employees.AddOrUpdate(e => e.EmployeeCode,
                new Employee { EmployeeCode = "CP01", FirstName = "Ella", LastName = "Hellman", Department = "Computer", Position = "Developer", Salary = 30000 },
                new Employee { EmployeeCode = "CP02", FirstName = "Bill", LastName = "Gate", Department = "Computer", Position = "Tester", Salary = 40000 },
                new Employee { EmployeeCode = "CP03", FirstName = "Bruno", LastName = "Mar", Department = "Computer", Position = "Frontend", Salary = 35000 },
                new Employee { EmployeeCode = "CP04", FirstName = "David", LastName = "Beckham", Department = "Computer", Position = "Internship", Salary = 45000 },
                new Employee { EmployeeCode = "HE01", FirstName = "Tom", LastName = "Cruise", Department = "Head", Position = "CEO", Salary = 100000 },
                new Employee { EmployeeCode = "HE02", FirstName = "Brad", LastName = "Bitt", Department = "Head", Position = "CTO", Salary = 80000 },
                new Employee { EmployeeCode = "SL01", FirstName = "Angelia", LastName = "Jolly", Department = "Sale", Position = "Manager", Salary = 35000 },
                new Employee { EmployeeCode = "SL02", FirstName = "Justin", LastName = "Bieber", Department = "Sale", Position = "Marketing", Salary = 30000 },
                new Employee { EmployeeCode = "SL03", FirstName = "Lady", LastName = "Gaga", Department = "Sale", Position = "Saler", Salary = 25000 },
                new Employee { EmployeeCode = "SL04", FirstName = "Christina", LastName = "Ronaldo", Department = "Sale", Position = "Editor", Salary = 40000 }
                );
        }
    }
}
