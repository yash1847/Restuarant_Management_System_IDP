using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.ProjectModel;
using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Models.ViewModels;
using Restuarant_Management_System_IDP.Repository.IRepository;

namespace Restuarant_Management_System_IDP.Controllers
{
    
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MenuViewModel menuViewModel;

        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            menuViewModel = new MenuViewModel()
            {
                CategoryList = _unitOfWork.Category.GetAll(includeProperties: "SubCategories,MenuItems").ToList(),
                categorySelectList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList(),
                SubCategoryDict = _unitOfWork.SubCategory.GetAll().GroupBy(x => x.CategoryId)
                                  .ToDictionary(g => g.Key, g => g.ToList().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList()),
            };
        }

        public IActionResult Index()
        {
            //List<Category> categories = _unitOfWork.Category.GetAll(includeProperties: "SubCategories,MenuItems").ToList();
            return View();
        }

        public IActionResult Menu()
        {
            menuViewModel.categorySelectList.Insert(0, new SelectListItem { Value = "showall", Text = "Show All" });
            //menuItemViewModel.SubCategoryList.Insert(0, new SelectListItem { Value = "showall", Text = "Show All" });
            //return View(menuItemViewModel);
            //var menuList = _unitOfWork.Category.GetAll(includeProperties: "SubCategories,MenuItems").ToList();
            return View(menuViewModel);
        }

        public IActionResult Profile()
        {
            return Content("User profile menu");
        }
    }
}
