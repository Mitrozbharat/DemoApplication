using Microsoft.AspNetCore.Mvc;

namespace DemoApplication.Controllers
{
    public class CurrencyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
