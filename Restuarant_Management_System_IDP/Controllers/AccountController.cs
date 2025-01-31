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
            ViewData["Message"] = TempData["Message"] == null ? "" : TempData["Message"].ToString();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //invalid
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.Username,model.Password,isPersistent:false,lockoutOnFailure:false);
            if (result.Succeeded)
            {

                int shoppingCartCount = _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == _userManager.GetUserId(User)).Count();
                HttpContext.Session.SetString(SD.ShoppingCartCount, shoppingCartCount.ToString());
                //SD.ShoppingCartCount = shoppingCartCount.ToString();
                if (User.IsInRole(SD.Admin))
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
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
                TempData["Message"] = "Registration Successfull";
                return RedirectToAction("Login");
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["Message"] = "You have successfully logged out";
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Profile()
        {
            var user = _userManager.GetUserAsync(User).Result;
            ProfileViewModel profileVM = new ProfileViewModel()
            {
                Id = user.Id,
                Name = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            return View(profileVM);            
        }

        [HttpPost]
        public async  Task<IActionResult> Profile(ProfileViewModel profileVM)
        {
            ModelState.Remove("Email");
            var user = await _userManager.GetUserAsync(User);

            if (!ModelState.IsValid)
            {
                profileVM = new ProfileViewModel()
                {
                    Id = user.Id,
                    Name = user.FullName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };
                return View(profileVM);
            }

            //var user = await _userManager.GetUserAsync(User)

            user.UserName = profileVM.Name;
            user.PhoneNumber = profileVM.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToAction("Profile");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            profileVM = new ProfileViewModel()
            {
                Id = user.Id,
                Name = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return View(profileVM);
        }


    }
}
