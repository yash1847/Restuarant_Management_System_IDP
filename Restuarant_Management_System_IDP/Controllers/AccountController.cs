using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restuarant_Management_System_IDP.Data;
using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Models.ViewModels;
using Restuarant_Management_System_IDP.Repository;
using Restuarant_Management_System_IDP.Repository.IRepository;

namespace Restuarant_Management_System_IDP.Controllers
{
    public class AccountController : Controller
    {
        private readonly RestaurantDbContext _db;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(RestaurantDbContext db,IUnitOfWork unitOfWork,SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            /*
            if (ModelState.IsValid)
            {
                //User? user = _unitOfWork.User.Get(u => u.UserName == model.Username);
                /*
                 * if (user == null)
                //{
                //    return Content("User does not exist");
                //}
                //if (user.Password != model.Password)
                //{
                //    return Content("Invalid user or password");
                //}
                //HttpContext.Session.SetString("username",model.Username);
                //HttpContext.Session.SetString("role", user.Role);
                //if(user.Role == "Admin")
                //{
                //    return RedirectToAction("Index", "Home");
                //}
                //else if(user.Role == "User")
                //{
                //    return RedirectToAction("Index", "Home");
                //}
                //return Content("Invalid role");
                
                
            }
        */
            //invalid
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.Username,model.Password,isPersistent:false,lockoutOnFailure:false);
            if (result.Succeeded)
            {
                var user = _db.Users.Where(u => u.UserName == model.Username).FirstOrDefault();
                HttpContext.Session.SetString("username", model.Username);
                return RedirectToAction("Index", "Home");
            }
            else if (result.IsLockedOut)
            {
                model.StatusMessage = "You currently locked out, Try after a while";
                return View(model);
            }
            else
            {
                model.StatusMessage = "Invlaid Password or userid";
                return View(model);
            }

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model) 
        {
            //invalid
            if (!ModelState.IsValid)
            {
                return View();
            }

            string role = Request.Form["rdUserRole"].ToString();
            //create user obj
            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.Contact
            };
            
            var result = await _userManager.CreateAsync(user,model.Password);

            if (result.Succeeded)
            {
                if (role == SD.Kitchen)
                {
                    await _userManager.AddToRoleAsync(user, SD.Kitchen);
                }
                else if (role == SD.Delivery)
                {
                    await _userManager.AddToRoleAsync(user, SD.Delivery);
                }
                else if (role == SD.Admin)
                {
                    await _userManager.AddToRoleAsync(user, SD.Admin);
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, SD.Customer);
                }

                Usertb newUser = new Usertb()
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
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            //HttpContext.Session.Clear();
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profile()
        {
            var username = HttpContext.Session.GetString("username");
            if(username == "admin")
            {
                return RedirectToAction("Profile", "Admin");
            }
            else
            {
                return RedirectToAction("Profile", "Customer");
            }
        }
    }
}
