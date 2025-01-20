using Microsoft.AspNetCore.Mvc;

namespace Restuarant_Management_System_IDP.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return Content("User profile menu");
        }
    }
}
