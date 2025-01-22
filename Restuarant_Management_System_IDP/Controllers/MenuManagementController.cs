using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restuarant_Management_System_IDP.Models;

namespace Restuarant_Management_System_IDP.Controllers
{
    [Authorize(Roles = SD.Admin)]
    public class MenuManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
