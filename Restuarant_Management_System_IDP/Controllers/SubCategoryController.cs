using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Repository.IRepository;

namespace Restuarant_Management_System_IDP.Controllers
{
    [Authorize(Roles = SD.Admin)]
    public class SubCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //string role = HttpContext.Session.GetString("role");
            //if (role != "Admin")
            if(!User.IsInRole(SD.Admin))
            {
                return RedirectToAction("Index", "Home");
            }
            var category_list = _unitOfWork.SubCategory.GetAll();
            return View(category_list);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
