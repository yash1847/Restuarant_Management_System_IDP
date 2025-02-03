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
    [Authorize(Roles = SD.Customer)]
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;


        public CustomerController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;

        }

        public IActionResult Index()
        {
            return View();
        }


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

            foreach (ShoppingCart cartItem in orderDetailsCart.cartList)
            {
                cartItem.MenuItem = _unitOfWork.MenuItem.Get(m => m.Id == cartItem.MenuItemId, includeProperties: "Category,SubCategory");
                orderDetailsCart.OrderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);
            }
            HttpContext.Session.SetString(SD.ShoppingCartCount, orderDetailsCart.cartList.Count.ToString());

            return View(orderDetailsCart);
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

        public IActionResult Summary()
        {

            ApplicationUser applicationUser = _userManager.GetUserAsync(User).Result;
            string userId = _userManager.GetUserId(User);

            SummaryViewModel summaryViewModel = new SummaryViewModel()
            {
                OrderHeader = new OrderHeader(),
                Address = _unitOfWork.Addresstb.Get(u => u.UserId == userId,includeProperties:"ApplicationUser"),
                cartList = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == userId, includeProperties: "MenuItem").ToList(),
            };

            if (summaryViewModel.Address == null)
            {
                return RedirectToAction("UpsertAddress");
            }

            foreach (ShoppingCart cartItem in summaryViewModel.cartList)
            {
                summaryViewModel.OrderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);
            }

            
            summaryViewModel.OrderHeader.OrderDate = DateTime.Now.Date;
            summaryViewModel.OrderHeader.PickupName = applicationUser.FullName;

            return View(summaryViewModel);

        }


        public IActionResult ViewAddress()
        {
            Addresstb address = _unitOfWork.Addresstb.Get(u => u.UserId == _userManager.GetUserId(User));
            ViewData["Message"] = TempData["Message"] as String;

            return View("Address/ViewAddress", address);
        }

        public IActionResult UpsertAddress()
        {
            Addresstb address = _unitOfWork.Addresstb.Get(u => u.UserId == _userManager.GetUserId(User), includeProperties: "ApplicationUser");
            if (address == null)
            {
                address = new Addresstb();
                address.ApplicationUser = _userManager.GetUserAsync(User).Result;
                address.UserId = _userManager.GetUserId(User);
            }
            return View("Address/Upsert",address);
        }

        [HttpPost]
        public IActionResult UpSertAddress()
        {
            Addresstb address = _unitOfWork.Addresstb.Get(u => u.UserId == _userManager.GetUserId(User), includeProperties: "ApplicationUser");
            if (address == null)
            {
                address = new Addresstb();
                address.ApplicationUser = _userManager.GetUserAsync(User).Result;
            }

            address.Address = Request.Form["Address"];
            address.City = Request.Form["City"];
            address.Pincode = Request.Form["Pincode"];
            address.UserId = Request.Form["UserId"];

            ModelState.Clear();
            TryValidateModel(address,nameof(address));

            if (ModelState.IsValid)
            {
                Addresstb addressFromDb = _unitOfWork.Addresstb.Get(u => u.UserId == _userManager.GetUserId(User), includeProperties: "ApplicationUser");
                if (addressFromDb == null)
                {
                    address.UserId = _userManager.GetUserId(User);
                    _unitOfWork.Addresstb.Add(address);
                    TempData["Message"] = "Address added successfully";

                }
                else
                {
                    addressFromDb.Address = address.Address;
                    addressFromDb.City = address.City;
                    addressFromDb.Pincode = address.Pincode;
                    _unitOfWork.Addresstb.Update(addressFromDb);
                    TempData["Message"] = "Address updated successfully";

                }
                _unitOfWork.Save();
                return RedirectToAction("ViewAddress");
            }

            return View("Address/Upsert", address);
        }
    }
}
