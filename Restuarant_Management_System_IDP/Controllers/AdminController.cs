using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restuarant_Management_System_IDP.Models;

namespace Restuarant_Management_System_IDP.Controllers
{
    [Authorize(Roles = SD.Admin)]
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return Content("Admin Dashboard");
        }
        public IActionResult Profile()
        {
            return Content("Admin Profile");
        }
    }
}
