using Microsoft.AspNetCore.Mvc;
using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Models.ViewModels;
using Restuarant_Management_System_IDP.Repository;
using Restuarant_Management_System_IDP.Repository.IRepository;

namespace Restuarant_Management_System_IDP.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User? user = _unitOfWork.User.Get(u => u.UserName == model.Username);
                if (user == null)
                {
                    return Content("User does not exist");
                }
                if (user.Password != model.Password)
                {
                    return Content("Invalid user or password");
                }
                if(user.Role == "Admin")
                {
                    return Content("Admin logged in");
                }
                else if(user.Role == "User")
                {
                    return Content("User logged in");
                }
                return Content("Invalid role");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            User? existing_user = _unitOfWork.User.Get(u => u.UserName == model.UserName);
            if (existing_user != null)
            {
                return Content("Existing user");
            }
            User newUser = new User()
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Password = model.Password,
                Email = model.Email,
                Contact = model.Contact,
                Role = "User"
            };
            _unitOfWork.User.Add(newUser);
            _unitOfWork.Save();
            return Content("User added successfully");
        }
    }
}
