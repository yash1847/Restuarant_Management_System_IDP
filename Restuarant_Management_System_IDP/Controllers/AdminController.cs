using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restuarant_Management_System_IDP.Data;
using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Models.ViewModels;
using Restuarant_Management_System_IDP.Repository.IRepository;

namespace Restuarant_Management_System_IDP.Controllers
{
    [Authorize(Roles = SD.Admin)]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            //_signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Dashboard()
        {
            return Content("Admin Dashboard");
        }
        public IActionResult Profile()
        {
            return Content("Admin Profile");
        }

        public async Task<IActionResult> GetUsers()
        {
            List<ApplicationUser> applicationUsers = _userManager.Users.ToList();
            List<UserViewModel> userViewModels = new List<UserViewModel>();
            string adminUserId = _userManager.GetUserId(User);

            foreach (var user in applicationUsers)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if(adminUserId == user.Id)
                {
                    continue;
                }
                var userisLocked = user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTimeOffset.UtcNow;

                UserViewModel userViewModel = new UserViewModel()
                {
                    UserId = user.Id,
                    Name = user.FullName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = userRoles.FirstOrDefault(),
                    isLocked = userisLocked
                };
                userViewModels.Add(userViewModel);
            }

            return View(userViewModels);
        }

        public async Task<IActionResult> LockUnlock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                //user is currently locked, we will unlock them
                user.LockoutEnd = DateTime.Now;
            }
            else
            {
                user.LockoutEnd = DateTime.Now.AddYears(100);
            }
            await _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(GetUsers));
        }


    }
}
