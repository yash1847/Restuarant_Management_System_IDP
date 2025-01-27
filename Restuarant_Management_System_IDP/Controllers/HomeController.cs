using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private IndexViewModel indexViewModel;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;

            indexViewModel = new IndexViewModel()
            {
                MenuItems = _unitOfWork.MenuItem.GetAll(includeProperties: "Category,SubCategory"),
                SubCategories = _unitOfWork.SubCategory.GetAll(includeProperties: "Category"),
                Categories = _unitOfWork.Category.GetAll()
            };
        }

        public IActionResult Index()
        {
            return View(indexViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
