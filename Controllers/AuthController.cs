using DemoApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoApplication.Controllers
{
    public class AuthController : Controller
    {

        private static List<User> _users = new List<User>
        {
            new User{Id=1,Username="bharatharkal", Email="bharat@gmail.com",Password="1234"},
            new User{Id=2,Username="Rohit", Email="rohit@gmail.com",Password="123456"}

        };



        [HttpGet]
        public IActionResult Login()
        {

            //return View(_users.ToList());
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _users.Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();

                if (user == null)
                {
                    ViewBag.Message = "Username/ Password wronge !";
                    return View();
                }
                return RedirectToAction("Index");
            }

            return View();
        }


        [HttpGet]
        public IActionResult Registration()
        {

            return View();
        }



        [HttpPost]
        public IActionResult Registration(User model)
        {
            if (ModelState.IsValid)
            {
                _users.Max(x => x.Id + 1);
                _users.Add(model);
                return RedirectToAction("Login");

            }
            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_users.ToList());
        }
       
       
        
    }
}
