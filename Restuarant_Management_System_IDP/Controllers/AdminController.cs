using Microsoft.AspNetCore.Mvc;

namespace Restuarant_Management_System_IDP.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return Content("Admin Dashboard");
            return View();
        }
    }
}
