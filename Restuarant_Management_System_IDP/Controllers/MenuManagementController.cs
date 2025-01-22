using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restuarant_Management_System_IDP.Data;
using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Repository.IRepository;

namespace Restuarant_Management_System_IDP.Controllers
{
    [Authorize(Roles = SD.Admin)]
    [Route("ManageMenu")]
    public class MenuManagementController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public MenuManagementController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("Category")]
        public IActionResult Index()
        {
            //return Content("Hello");
            var category_list = _unitOfWork.Category.GetAll();
            return RedirectToAction("Index", "Category");
            //return View("Category",category_list);
        }

        [Route("Category/Create")]
        public IActionResult Create()
        {
            return RedirectToAction("Create", "Category");
        }

        //[Route("/")]
        [HttpPost]
        [Route("Category/Create")]
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





    }
}
