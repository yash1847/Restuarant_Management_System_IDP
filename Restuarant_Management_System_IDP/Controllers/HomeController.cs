using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Models.ViewModels;
using Restuarant_Management_System_IDP.Repository.IRepository;
using System.Diagnostics;

namespace Restuarant_Management_System_IDP.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MenuViewModel menuViewModel;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;

            menuViewModel = new MenuViewModel()
            {
                CategoryList = _unitOfWork.Category.GetAll(x => x.MenuItems.Count() > 0, includeProperties: "SubCategories,MenuItems").ToList(),
                categorySelectList = _unitOfWork.Category.GetAll(x => x.MenuItems.Count() > 0).Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList(),
                SubCategoryDict = _unitOfWork.SubCategory.GetAll(x => x.MenuItems.Count() > 0).GroupBy(x => x.CategoryId)
                                              .ToDictionary(g => g.Key, g => g.ToList().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList()),
            };

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Menu()
        {
            menuViewModel.categorySelectList.Insert(0, new SelectListItem { Value = "showall", Text = "Show All" });
            return View(menuViewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
