using Microsoft.AspNetCore.Mvc;

namespace Restuarant_Management_System_IDP.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }
    }
}
