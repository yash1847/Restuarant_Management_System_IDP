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

        [HttpPost]
        public IActionResult Register(IFormCollection frmc) 
        {
            if (!ModelState.IsValid)
            {
                //invalid
                return View();
            }

            string customerId = frmc["customerId"];
            string email = frmc["email"];
            string password = frmc["password"];
            string firstname = frmc["firstname"];
            string lastname = frmc["lastname"];
            string contact = frmc["contact"];
            string address = frmc["address"];
            string city = frmc["city"];
            string pincode = frmc["pincode"];
            return Content(customerId);
        }
    }
}
