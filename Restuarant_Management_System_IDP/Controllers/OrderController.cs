using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Models.ViewModels;
using Restuarant_Management_System_IDP.Repository.IRepository;

namespace Restuarant_Management_System_IDP.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly RoleManager<IdentityRole> _roleManager;


        public OrderController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            //_signInManager = signInManager;
            //_roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PlaceOrder()
        {
            string userId = _userManager.GetUserId(User);
            ApplicationUser applicationUser = _userManager.GetUserAsync(User).Result;

            OrderDetailViewModel orderDetailVM = new OrderDetailViewModel()
            {
                OrderHeader = new OrderHeader(),
                orderDetails = new List<OrderDetail>()
            };

            //OrderHeader orderHeader = new OrderHeader();
            //List<OrderDetail> orderDetails = new List<OrderDetail>();
            List<ShoppingCart> cartList = _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == userId, includeProperties: "MenuItem").ToList();
            Addresstb deliverAddress = _unitOfWork.Addresstb.Get(x => x.UserId == userId);

            foreach (ShoppingCart cartItem in cartList)
            {
                OrderDetail orderDetail = new OrderDetail();

                orderDetail.MenuItemId = cartItem.MenuItemId;
                orderDetail.Count = cartItem.Count;
                orderDetail.Price = cartItem.MenuItem.Price;
                orderDetail.Name = cartItem.MenuItem.Name;
                orderDetail.Description = cartItem.MenuItem.Description;
                //orderDetails.Add(orderDetail);
                orderDetailVM.orderDetails.Add(orderDetail);

                //orderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);
                orderDetailVM.OrderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);


            }

       

            orderDetailVM.OrderHeader.UserId = userId;
            orderDetailVM.OrderHeader.PickupName = applicationUser.FullName;
            orderDetailVM.OrderHeader.OrderDate = DateTime.Now.Date;
            orderDetailVM.OrderHeader.OrderTime = DateTime.Now.AddMinutes(30);
            orderDetailVM.OrderHeader.Status = SD.StatusSubmitted;
            orderDetailVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            orderDetailVM.OrderHeader.DeliverAddress = deliverAddress.Address;
            orderDetailVM.OrderHeader.DeliverCity = deliverAddress.City;
            orderDetailVM.OrderHeader.DeliverPinCode = deliverAddress.Pincode;

            orderDetailVM.OrderHeader.ApplicationUser = applicationUser;

            //create orderheader 

            //_unitOfWork.OrderHeader.Add(orderHeader);
            _unitOfWork.OrderHeader.Add(orderDetailVM.OrderHeader);
            _unitOfWork.Save();

            //get Orderheader Id
            foreach (OrderDetail detail in orderDetailVM.orderDetails)
            {
                detail.OrderId = orderDetailVM.OrderHeader.Id;
                _unitOfWork.OrderDetail.Add(detail);
                _unitOfWork.Save();
            }

            //delete cartitems
            foreach (ShoppingCart cartItem in cartList)
            {
                _unitOfWork.ShoppingCart.Delete(cartItem);
                _unitOfWork.Save();
            }

            return View("OrderConfirmation", orderDetailVM);
        }

        public IActionResult OrderHistory()
        {
            List<OrderDetailViewModel> orders = new List<OrderDetailViewModel>();
            List<OrderHeader> orderHeaders = _unitOfWork.OrderHeader.GetAll(x => x.UserId == _userManager.GetUserId(User), includeProperties: "ApplicationUser").OrderByDescending(x => x.Id).ToList();

            foreach (OrderHeader orderHeader in orderHeaders)
            {
                OrderDetailViewModel individual = new OrderDetailViewModel();
                individual.OrderHeader = orderHeader;
                individual.orderDetails = _unitOfWork.OrderDetail.GetAll(x => x.OrderId == orderHeader.Id).ToList();
                orders.Add(individual);
            }

            return View(orders);

        }

        public IActionResult OrderDetails(int id)
        {
            OrderDetailViewModel orderDetailsViewModel = new OrderDetailViewModel()
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(x => x.Id == id),
                orderDetails = _unitOfWork.OrderDetail.GetAll(o => o.OrderId == id).ToList(),
            };

            orderDetailsViewModel.OrderHeader.ApplicationUser = _userManager.GetUserAsync(User).Result;

            return View("OrderDetails", orderDetailsViewModel);
        }

        [Authorize(Roles = SD.Admin + "," + SD.Kitchen)]
        public IActionResult ManageOrder()
        {
            List<OrderDetailViewModel> orders = new List<OrderDetailViewModel>();
            List<OrderHeader> orderHeaders = _unitOfWork.OrderHeader.GetAll(o => o.Status == SD.StatusSubmitted || o.Status == SD.StatusInProcess, includeProperties: "ApplicationUser").OrderBy(x => x.Id).ToList();
            foreach (OrderHeader orderHeader in orderHeaders)
            {
                OrderDetailViewModel individual = new OrderDetailViewModel();
                individual.OrderHeader = orderHeader;
                individual.orderDetails = _unitOfWork.OrderDetail.GetAll(x => x.OrderId == orderHeader.Id).ToList();
                orders.Add(individual);
            }
            return View(orders);
        }

        [Authorize(Roles = SD.Admin + "," + SD.Kitchen)]
        public IActionResult OrderPrepare(int? OrderId)
        {
            if (OrderId == null) { return NotFound(); }

            OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(x => x.Id == OrderId,tracked:true);

            if (orderHeader == null) { return NotFound(); }

            orderHeader.Status = SD.StatusInProcess;
            _unitOfWork.Save();
            return RedirectToAction("ManageOrder", "Order");
        }

        [Authorize(Roles = SD.Admin + "," + SD.Kitchen)]
        public IActionResult OrderReady(int? OrderId)
        {
            if (OrderId == null) { return NotFound(); }

            OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(x => x.Id == OrderId,tracked:true);

            if (orderHeader == null) { return NotFound(); }

            orderHeader.Status = SD.StatusReady;

            _unitOfWork.Save();

            return RedirectToAction("ManageOrder");
        }

        [Authorize(Roles = SD.Admin + "," + SD.Delivery)]
        public IActionResult OrderPickup()
        {
            List<OrderDetailViewModel> orders = new List<OrderDetailViewModel>();
            List<OrderHeader> orderHeaders = _unitOfWork.OrderHeader.GetAll(u => u.Status == SD.StatusReady, includeProperties: "ApplicationUser").OrderBy(x => x.Id).ToList();
            
            foreach (OrderHeader orderHeader in orderHeaders)
            {
                OrderDetailViewModel individual = new OrderDetailViewModel();
                individual.OrderHeader = orderHeader;
                individual.orderDetails = _unitOfWork.OrderDetail.GetAll(x => x.OrderId == orderHeader.Id).ToList();
                orders.Add(individual);
            }

            return View(orders);

        }

        [Authorize(Roles = SD.Admin + "," + SD.Delivery)]
        public IActionResult OrderDelivery(int? OrderId)
        {
            if (OrderId == null) { return NotFound(); }

            OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(x => x.Id == OrderId,tracked:true);

            if (orderHeader == null) { return NotFound(); }

            orderHeader.Status = SD.StatusDelivered;

            _unitOfWork.Save();

            return RedirectToAction("OrderPickup");
        }

        [Authorize(Roles = SD.Admin)]
        public IActionResult OrderCancel(int? OrderId)
        {
            if (OrderId == null) { return NotFound(); }

            OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(x => x.Id == OrderId,tracked:true);

            if (orderHeader == null) { return NotFound(); }

            orderHeader.Status = SD.StatusCancelled;
            _unitOfWork.Save();

            return RedirectToAction("ManageOrder");
        }

    }
}
