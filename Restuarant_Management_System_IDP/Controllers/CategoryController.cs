using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Repository.IRepository;

namespace Restuarant_Management_System_IDP.Controllers
{
    [Authorize(Roles = SD.Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //string role = HttpContext.Session.GetString("role");
            if (!User.IsInRole(SD.Admin))
            {
                return RedirectToAction("Index", "Home");
            }
            var category_list = _unitOfWork.Category.GetAll();
            return View(category_list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid) 
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return Content("Not found");
            }
            Category category = _unitOfWork.Category.Search(id);
            if (category == null)
            {
                return Content("Not found");
            }
            return View(category);
        }

    }
}
