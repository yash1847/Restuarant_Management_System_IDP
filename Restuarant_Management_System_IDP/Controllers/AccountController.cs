using Microsoft.AspNetCore.Mvc;
using Restuarant_Management_System_IDP.Models;

namespace Restuarant_Management_System_IDP.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(IFormCollection frmc)
        {  

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Usertable()
        {
            return View();
        }

        public IActionResult Usertable()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(IFormCollection frmc) 
        {
            if (!ModelState.IsValid)
            {
                //invalid
                return View();
            }

            string customerId = frmc["User.CustomerId"];
            return Content(customerId);
        }
    }
}
