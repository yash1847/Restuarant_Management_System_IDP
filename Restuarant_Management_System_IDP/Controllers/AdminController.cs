using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
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
            var adminDashboardVM = new AdminDashboardViewModel();
            getMetrics(adminDashboardVM);
            return View(adminDashboardVM);
        }

        private void getMetrics(AdminDashboardViewModel adminDashboardVM)
        {
            adminDashboardVM.OrdersSubmitted = _unitOfWork.OrderHeader.GetAll(x => x.Status == SD.StatusSubmitted).Count();
            adminDashboardVM.OrdersonDelivery = _unitOfWork.OrderHeader.GetAll(x => x.Status == SD.StatusReady).Count();
            adminDashboardVM.OrdersCompleted = _unitOfWork.OrderHeader.GetAll(x => x.Status == SD.StatusDelivered).Count();
            adminDashboardVM.UsersRegistered = _userManager.GetUsersInRoleAsync(SD.Customer).Result.Count;
            adminDashboardVM.EmployeesRegistered = _userManager.Users.Count() - adminDashboardVM.UsersRegistered;
            adminDashboardVM.TotalSales = _unitOfWork.OrderHeader.GetAll().Sum(x => x.OrderTotal);
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
