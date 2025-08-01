using DemoApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoApplication.Controllers
{
    public class EmployeeController : Controller
    {

        public static List<Employee> employees = new List<Employee>
        {
            new Employee {Id=1,Email="bharat@gmail.com", Role="User"},
            new Employee {Id=2,Email="ram@gmail.com", Role="User"},
            new Employee {Id=3,Email="rohit@gmail.com", Role="User"}
        };

        public IActionResult Index()
        {
            var model = employees.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Employee model)
        {

            //if (model.Email != null && model.Role != null)
            //{

            //}
           

            employees.Max(x => x.Id + 1);
            employees.Add(model);
            return RedirectToAction("Index");

        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = employees.Where(x => x.Id == id).FirstOrDefault();

            if (employee == null) {

                return NotFound();
            }
            return View(employee);
        }
        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            //Employee emp = new Employee();

            var employee = employees.Where(x => x.Id ==model.Id).FirstOrDefault();

            if (employee == null) {

                NotFound();
            }
            employee.Email = model.Email;
            employee.Role = model.Role; 
          
            return RedirectToAction("Index");


        }
        public IActionResult Delete(int id)
        {
            //Employee emp = new Employee();

            var employee = employees.Where(x => x.Id == id).FirstOrDefault();

            if (employee == null)
            {

                NotFound();
            }
            employees.Remove(employee); 

            return RedirectToAction("Index");


        }

    }
}
