using Microsoft.AspNetCore.Mvc;
using Restuarant_Management_System_IDP.Repository.IRepository;

namespace Restuarant_Management_System_IDP.Controllers
{
    public class MenuItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public MenuItemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
