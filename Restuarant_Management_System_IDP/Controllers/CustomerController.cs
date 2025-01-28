using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CustomerController(IUnitOfWork unitOfWork, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

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

        [Authorize(Roles = SD.Customer)]
        public IActionResult AddToCart(int id) 
        {
            string UserId = _userManager.GetUserId(User);
            ShoppingCart? cartItem = _unitOfWork.ShoppingCart.Get(c => c.ApplicationUserId == UserId && c.MenuItemId == id);

            if (cartItem == null)
            {
                cartItem = new ShoppingCart()
                {
                    ApplicationUserId = UserId,
                    MenuItemId = id,
                    Count = 1
                };
                _unitOfWork.ShoppingCart.Add(cartItem);
            }
            else
            {
                cartItem.Count += 1;
                _unitOfWork.ShoppingCart.Update(cartItem);
            }

            _unitOfWork.Save();
            return RedirectToAction("Cart");
        }

        public IActionResult Cart()
        {
            //List<ShoppingCart> cartItems = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == _userManager.GetUserId(User), includeProperties: "MenuItem").ToList();
            OrderDetailsCart orderDetailsCart = new OrderDetailsCart()
            {
                OrderHeader = new OrderHeader(),
                cartList = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == _userManager.GetUserId(User), includeProperties: "MenuItem").ToList(),
            };

            foreach(ShoppingCart cartItem in orderDetailsCart.cartList)
            {
                cartItem.MenuItem = _unitOfWork.MenuItem.Get(m => m.Id == cartItem.MenuItemId, includeProperties: "Category,SubCategory");
                orderDetailsCart.OrderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);
            }

            return View(orderDetailsCart);
            //return Json(new { cart = cartItems });
        }

        public IActionResult Additem(int id)
        {
            ShoppingCart cartItem = _unitOfWork.ShoppingCart.Get(c => c.ApplicationUserId == _userManager.GetUserId(User) && c.MenuItemId == id);
            cartItem.Count += 1;
            _unitOfWork.ShoppingCart.Update(cartItem);
            _unitOfWork.Save();
            return RedirectToAction("Cart");
        }

        public IActionResult Subtractitem(int id)
        {
            ShoppingCart cartItem = _unitOfWork.ShoppingCart.Get(c => c.ApplicationUserId == _userManager.GetUserId(User) && c.MenuItemId == id);
            if (cartItem.Count == 1)
            {
                _unitOfWork.ShoppingCart.Delete(cartItem);
            }
            else
            {
                cartItem.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cartItem);
            }
            _unitOfWork.Save();
            return RedirectToAction("Cart");
        }
        public IActionResult Deleteitem(int id)
        {
            ShoppingCart cartItem = _unitOfWork.ShoppingCart.Get(c => c.ApplicationUserId == _userManager.GetUserId(User) && c.MenuItemId == id);

            //delete the item from the cart
            _unitOfWork.ShoppingCart.Delete(cartItem);
            _unitOfWork.Save();

            return RedirectToAction("Cart");
        }


        public IActionResult Profile()
        {
            return Content("User profile menu");
        }
    }
}
