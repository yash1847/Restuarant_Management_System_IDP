using Microsoft.AspNetCore.Mvc;
using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Repository;
using Restuarant_Management_System_IDP.Repository.IRepository;

namespace Restuarant_Management_System_IDP.Controllers
{
    public class AccountController : Controller
    {
        //LoginRepository loginrepo;
        //UserRepository userrepo;

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
        public IActionResult Login(IFormCollection frmc)
        {  

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Usertable()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(IFormCollection frmc) 
        {
            if (!ModelState.IsValid)
            {
                //invalid
                return View();
            }

            User user = new User()
            {
                CustomerId = frmc["User.CustomerId"],
                FirstName = frmc["User.FirstName"],
                LastName = frmc["User.LastName"],
                Email = frmc["User.Email"],
                Contact = frmc["User.Contact"]
            };
            Login login = new Login()
            {
                LoginId = frmc["User.CustomerId"],
                Password = frmc["Login.Password"],
                Role = Roles.Customer
            };
            //loginrepo.Add(login);
            //userrepo.Add(user);
            _unitOfWork.Login.Add(login);
            _unitOfWork.User.Add(user);
            return Content("Success");
        }
    }
}
