using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyDemoPractical.Models;
using MyDemoPractical.Data;

namespace MyDemoPractical.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            EmployeeDetails objEmployeeDetails = new EmployeeDetails();/*amit*/
            List<Employee> lstEmployee = new List<Employee>();
            List<Department> lstDepartment = new List<Department>();
            Department objDepartment = null;
            Employee objEmployee = null;
            using (ORGEntities1 objORGEntities = new ORGEntities1())
            {
                var depts = objORGEntities.Depts.ToList();
                foreach (var item in depts)
                {
                    objDepartment = new Department();
                    objDepartment.DeptId = item.DeptId;
                    objDepartment.DeptName = item.DeptName;
                    objDepartment.IsSelect = false;
                    lstDepartment.Add(objDepartment);
                }
                var employees = (from emp in objORGEntities.Emps
                                  join dept in objORGEntities.Depts on emp.DeptId equals dept.DeptId
                                  select new
                                  {
                                      Id = emp.Id,
                                      Name = emp.Name,
                                      DeptName = dept.DeptName
                                  });
                foreach (var emp in employees)
                {
                    objEmployee = new Employee();
                    objEmployee.Id = emp.Id;
                    objEmployee.Name = emp.Name;
                    objEmployee.DeptName = emp.DeptName;
                    lstEmployee.Add(objEmployee);
                }
            }
            objEmployeeDetails.EmployeeData = lstEmployee;
            objEmployeeDetails.DepartmentData = lstDepartment;
            return View(objEmployeeDetails);
        }

        [HttpPost]
        public ActionResult Index(string deptName)
        {
            deptName = "IT";
            EmployeeDetails objEmployeeDetails = new EmployeeDetails();
            List<Employee> lstEmployee = new List<Employee>();
            List<Department> lstDepartment = new List<Department>();
            Employee objEmployee = null;
            using (ORGEntities1 objORGEntities = new ORGEntities1())
            {
                var employees = (from emp in objORGEntities.Emps
                                 join dept in objORGEntities.Depts on emp.DeptId equals dept.DeptId
                                 where dept.DeptName ==deptName
                                 select new
                                 {
                                     Id = emp.Id,
                                     Name = emp.Name,
                                     DeptName = dept.DeptName
                                 });
                foreach (var emp in employees)
                {
                    objEmployee = new Employee();
                    objEmployee.Id = emp.Id;
                    objEmployee.Name = emp.Name;
                    objEmployee.DeptName = emp.DeptName;
                    lstEmployee.Add(objEmployee);
                }
            }
            objEmployeeDetails.EmployeeData = lstEmployee;
            objEmployeeDetails.DepartmentData = lstDepartment;
            return View(objEmployeeDetails);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}