using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using NuGet.Protocol;
using Restuarant_Management_System_IDP.Data;
using Restuarant_Management_System_IDP.Models;
using Restuarant_Management_System_IDP.Models.ViewModels;
using Restuarant_Management_System_IDP.Repository.IRepository;

namespace Restuarant_Management_System_IDP.Controllers
{
    [Authorize(Roles = SD.Admin)]
    public class ManageMenuController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        private MenuItemViewModel menuItemVM { get; set; }

        [BindProperty]
        private SubCategoryAndCategoryViewModel subCategoryAndCategoryVM { get; set; }
        public ManageMenuController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

            subCategoryAndCategoryVM = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }),
                SubCategory = new SubCategory()
            };

            menuItemVM = new MenuItemViewModel()
            {
                CategoryList = subCategoryAndCategoryVM.CategoryList.ToList(),
                MenuItem = new MenuItem(),
            };



        }

        //[Route("Category")]
        public IActionResult Category()
        {
            var category_list = _unitOfWork.Category.GetAll();
            return View("Category/Index", category_list);
        }

        //[Route("Category/Create")]
        public IActionResult CategoryCreate()
        {
            return View("Category/Create");
        }

        [HttpPost]
        //[Route("Category/Create")]
        public IActionResult CategoryCreate(Category category)
        {
            if (ModelState.IsValid)
            {
                //check if category name already exists
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                return RedirectToAction("Category");
            }
            ModelState.AddModelError(string.Empty, "Invalid");

            return View("Category/Create", category);
        }

        //[Route("Category/Details/id")]
        public IActionResult CategoryDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category? category = _unitOfWork.Category.Search(id);
            if (category == null)
            {
                return NotFound();
            }
            return View("Category/Details", category);
        }

        //[Route("Category/Edit/id")]
        public IActionResult CategoryEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category? category = _unitOfWork.Category.Get(x => x.Id == id,tracked:false);
            if (category == null)
            {
                return NotFound();
            }
            return View("Category/Edit",category);
        }

        [HttpPost]
        public IActionResult CategoryEdit(Category category)
        {
            if (!ModelState.IsValid)
            {
                //add model error
                return View("Category/Edit",category);
            }

            //check if category name already exists
            bool categoryNameExists = _unitOfWork.Category.Get(c => c.Id != category.Id && c.Name == category.Name) != null;

            if (categoryNameExists)
            {
                ModelState.AddModelError(string.Empty, "Category Name already exists");
                return View("Category/Edit", category);
            }
            
            Category categoryFromdB = _unitOfWork.Category.Get(c => c.Id == category.Id,tracked:true);

            //_unitOfWork.Category.Update(category);
            categoryFromdB.Name = category.Name;
            //_unitOfWork.Category.Update(categoryFromdB);
            _unitOfWork.Save();
            return RedirectToAction("Category");
        }

        [HttpGet]
        public IActionResult CategoryDelete(int? id)
        {
            //test once menu items are added.
            if(id == null)
            {
                return Json(new {success = false, message = "Category not found"});
            }

            Category? category = _unitOfWork.Category.Get(x => x.Id == id,includeProperties:"MenuItems",tracked:true);
            if(category == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }
            if (category.MenuItems.Any())
            {
                return Json(new { success = false, message = "Error: Category contains menu items!" });
            }
            try
            {
                _unitOfWork.Category.Delete(category);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = true, message = "Item deleted successfully." });
        }

        [HttpGet]
        public IActionResult SubCategory()
        {
            var subcategory_list = _unitOfWork.SubCategory.GetAll(includeProperties:"Category");
            return View("SubCategory/Index",subcategory_list);
        }

        [HttpPost]
        public IActionResult SearchSubCategory(IFormCollection frm)
        {
            string? searchQuery = (string?)frm["searchQuery"];
            string? searchType = (string?)frm["searchType"];
            string? buttonPressed = (string?)frm["button"];

            //Console.WriteLine(frm.ToString());
            System.Diagnostics.Debug.WriteLine(frm.ToString());

            //Show all sub categories
            if(buttonPressed == "Show all")
            {
                return RedirectToAction("SubCategory","ManageMenu");
            }

            List<SubCategory> subcategory_list;

            if (String.IsNullOrEmpty(searchQuery))
            {
                ModelState.AddModelError(String.Empty, "Search field is empty");
                //return to the view with the error
                subcategory_list = _unitOfWork.SubCategory.GetAll(includeProperties:"Category").ToList();
                return View("SubCategory/Index",subcategory_list);
            }

            searchQuery = searchQuery.ToLower();

            if (searchType == "Category")
            {
                //searches based on the category
                subcategory_list = _unitOfWork.SubCategory.GetAll(s => s.Category.Name.ToLower().StartsWith(searchQuery), includeProperties: "Category").ToList();
            }
            else
            {
                //searches based on subcategory
                subcategory_list = _unitOfWork.SubCategory.GetAll(s => s.Name.ToLower().StartsWith(searchQuery),includeProperties: "Category").ToList();
            }

            return View("SubCategory/Index", subcategory_list);

        }

        public IActionResult SubCategoryCreate()
        {
            //List<Category> categories = _unitOfWork.Category.GetAll();
            //SubCategoryAndCategoryViewModel model = new SubCategoryAndCategoryViewModel()
            //{
            //    CategoryList = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name}),
            //    SubCategory = new SubCategory(),
            //};
            return View("SubCategory/Create",subCategoryAndCategoryVM);
        }
        
        [HttpPost]
        public IActionResult SubCategoryCreate(SubCategoryAndCategoryViewModel model)
        {

            //bool subcategoryExists = _unitOfWork.SubCategory.GetAll().Where(s => s.CategoryId == model.SubCategory.CategoryId && s.Name == model.SubCategory.Name).Any();
            bool subcategoryExists = _unitOfWork.SubCategory.Get(s => s.CategoryId == model.SubCategory.CategoryId && s.Name == model.SubCategory.Name) != null;
            if (subcategoryExists)
            {
                model.StatusMessage = "Error: Sub Category with the same already exists under this category, Please use another Name";
                
                subCategoryAndCategoryVM.SubCategory = model.SubCategory;
                subCategoryAndCategoryVM.StatusMessage = model.StatusMessage;

                return View("SubCategory/Create", subCategoryAndCategoryVM);
            }
            else
            {
                _unitOfWork.SubCategory.Add(model.SubCategory);    
                _unitOfWork.Save();
                return RedirectToAction("SubCategory");
            }
        }

        [HttpGet]
        public IActionResult GetSubCategory(int id)
        {
            //to display in the Category create view
            List<SubCategory> subCategories = _unitOfWork.SubCategory.GetAll();
            subCategories = (from s in subCategories
                             where s.CategoryId == id
                             select s).ToList();
            return Json(new SelectList(subCategories,"Id","Name"));
        }

        [HttpGet]
        public IActionResult SubCategoryDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SubCategory subcategory = _unitOfWork.SubCategory.Get(s => s.Id == id, includeProperties: "Category");

            if (subcategory == null)
            {
                return NotFound();
            }
            //also show the menu items in here
            return View("SubCategory/Details",subcategory);
        }

        [HttpGet]
        public IActionResult SubCategoryEdit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            SubCategory subcategory = _unitOfWork.SubCategory.Get(s => s.Id == id);
            if (subcategory == null)
            {
                return NotFound();
            }
            List<Category> categories = _unitOfWork.Category.GetAll();
            SubCategoryAndCategoryViewModel model = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }),
                SubCategory = subcategory,
            };
            
            return View("SubCategory/Edit", model);
        }

        [HttpPost]
        public IActionResult SubCategoryEdit(SubCategoryAndCategoryViewModel model)
        {
            //bool subcategoryExists = _unitOfWork.SubCategory.GetAll().Where(s => s.CategoryId == model.SubCategory.CategoryId && s.Name == model.SubCategory.Name).Any();
            bool subcategoryExists = _unitOfWork.SubCategory.Get(s => s.CategoryId == model.SubCategory.CategoryId && s.Name == model.SubCategory.Name) != null;
            if (subcategoryExists)
            {
                model.StatusMessage = "Error: Sub Category with the same already exists under this category, Please use another Name";
            }
            else
            {
                //model.SubCategory.Category = _unitOfWork.Category.Search(model.SubCategory.CategoryId);
                _unitOfWork.SubCategory.Update(model.SubCategory);
                _unitOfWork.Save();
                return RedirectToAction("SubCategory");
            }

            //List<Category> categories = _unitOfWork.Category.GetAll();
            //SubCategoryAndCategoryViewModel modelVM = new SubCategoryAndCategoryViewModel()
            //{
            //    CategoryList = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }),
            //    SubCategory = model.SubCategory,
            //    StatusMessage = model.StatusMessage
            //};
            subCategoryAndCategoryVM.SubCategory = model.SubCategory;
            subCategoryAndCategoryVM.StatusMessage = model.StatusMessage;
            return View("SubCategory/Edit", subCategoryAndCategoryVM);
        }

        //not yet implemented
        [HttpGet]
        public IActionResult SubCategoryDelete(int? id)
        {
            if(id == null)
            {
                return Json(new { success = false, message = "Item not found" });
            }
            
            SubCategory subCategory = _unitOfWork.SubCategory.Get(x => x.Id == id,includeProperties:"MenuItems");

            if(subCategory == null)
            {
                return Json(new { success = false, message = "Item not found" });
            }

            if (subCategory.MenuItems.Any())
            {
                return Json(new { success = false, message = "Error: SubCategory contains Menu Items" });
            }

            try
            {
                _unitOfWork.SubCategory.Delete(subCategory);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return Json(new {success=false, message = "An error occured while deleting"});
            }

            return Json(new { success = true, message = "Item deleted successfully" });
        }

        //get method
        //index property
        public IActionResult MenuItem()
        {
            //display all menu items
            //var menuitems = _unitOfWork.MenuItem.GetAll(includeProperties: "Category,SubCategory");
            bool menuItemsExist = _unitOfWork.MenuItem.GetAll().Any();
            ViewBag.count = menuItemsExist ? true : false;
            return View("MenuItem/Index");
        }

        [HttpGet]
        public IActionResult GetMenuItems()
        {
            var menuItems = _unitOfWork.MenuItem.GetAll(includeProperties: "Category,SubCategory").ToList();
            return Json(new { data = menuItems });
        }

        [HttpGet]
        public IActionResult MenuItemCreate()
        {
            return View("MenuItem/Create", menuItemVM);
        }

        [HttpPost]
        public IActionResult MenuItemCreate(MenuItemViewModel model)
        {

            model.MenuItem.SubCategoryId = int.Parse(Request.Form["SubCategoryId"].ToString());
            bool menuItemExists = _unitOfWork.MenuItem.Get(x => x.Name == model.MenuItem.Name && x.CategoryId == model.MenuItem.CategoryId && x.SubCategoryId == model.MenuItem.SubCategoryId) != null;
            //menuItemExists = true;
            if (menuItemExists)
            {
                model.StatusMessage = "Error: Menu Item with the same name already exists under this category and subcategory, Please use another Name";
                menuItemVM.StatusMessage = model.StatusMessage;
                return View("MenuItem/Create", menuItemVM);
            }

            //model.MenuItem.SubCategory = _unitOfWork.SubCategory.Search(model.MenuItem.SubCategoryId);

            _unitOfWork.MenuItem.Add(model.MenuItem);
            _unitOfWork.Save();

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            MenuItem menuItemFromDb = _unitOfWork.MenuItem.Search(model.MenuItem.Id);

            var files = Request.Form.Files;
            if (files.Count > 0)
            {
                // files has been uploaded
                var uploads = Path.Combine(wwwRootPath, "images/MenuItemsImages");  // The images Folder , Location
                var extension = Path.GetExtension(files[0].FileName);   // use the just first one upload and get it's extension
                string total_path = Path.Combine(uploads, menuItemFromDb.Id + "_" + menuItemFromDb.Name + extension);

                using (var filesStream = new FileStream(total_path, FileMode.Create))
                {
                    // copy the first file to this location (FileStream) with new name (the id and name of item)
                    files[0].CopyTo(filesStream);
                }

                menuItemFromDb.Image = @"\images\MenuItemsImages\" + menuItemFromDb.Id + "_" + menuItemFromDb.Name + extension;
                //return Content("File uploaded");
            }
            else
            {
                menuItemFromDb.Image = @"\images\" + SD.DefaultFoodImage;
            }

            _unitOfWork.Save();
            return RedirectToAction("MenuItem");
        }

        [HttpGet]
        public IActionResult MenuItemDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            menuItemVM.MenuItem = _unitOfWork.MenuItem.Search(id);

            if (menuItemVM.MenuItem == null)
            {
                return NotFound();
            }

            //why is sub category not being referenced ?
            menuItemVM.MenuItem.SubCategory = _unitOfWork.SubCategory.Search(menuItemVM.MenuItem.SubCategoryId);

            return View("MenuItem/Details", menuItemVM);
        }

        [HttpGet]
        public IActionResult MenuItemEdit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            menuItemVM.MenuItem = _unitOfWork.MenuItem.Search(id);
            
            if(menuItemVM.MenuItem == null)
            {
                return NotFound();
            }

            menuItemVM.MenuItem.SubCategory = _unitOfWork.SubCategory.Search(menuItemVM.MenuItem.SubCategoryId);
            return View("MenuItem/Edit",menuItemVM);
        }

        [HttpPost]
        public IActionResult MenuItemEdit(MenuItemViewModel model)
        {
            model.MenuItem.SubCategoryId = int.Parse(Request.Form["SubCategoryId"].ToString());
            bool menuItemExists = _unitOfWork.MenuItem.Get(m => m.Name == model.MenuItem.Name && m.Id != model.MenuItem.Id && m.CategoryId == model.MenuItem.CategoryId && m.SubCategoryId == model.MenuItem.SubCategoryId) != null;

            if (menuItemExists)
            {
                model.StatusMessage = "Error: Menu Item with the same name already exists under this category and subcategory, Please use another Name";
                menuItemVM.StatusMessage = model.StatusMessage;
                return View("MenuItem/Edit", menuItemVM);
            }

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            var files = Request.Form.Files;

            MenuItem menuItemFromDb = _unitOfWork.MenuItem.Search(model.MenuItem.Id);

            //updating properties
            menuItemFromDb.Name = model.MenuItem.Name;
            menuItemFromDb.Description = model.MenuItem.Description;
            menuItemFromDb.Price = model.MenuItem.Price;
            menuItemFromDb.CategoryId = model.MenuItem.CategoryId;
            menuItemFromDb.SubCategoryId = model.MenuItem.SubCategoryId;

            if (files.Count > 0)
            {
                // files has been uploaded
                var uploads = Path.Combine(wwwRootPath, "images/MenuItemsImages");  // The images Folder , Location
                var extension = Path.GetExtension(files[0].FileName);   // use the just first one upload and get it's extension

                //delete the old image
                var imagePath = Path.Combine(wwwRootPath, menuItemFromDb.Image.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                using (var filesStream = new FileStream(Path.Combine(uploads, menuItemFromDb.Id + "_" + menuItemFromDb.Name + extension), FileMode.Create))
                {
                    // copy the first file to this location (FileStream) with new name (the id of item)
                    files[0].CopyTo(filesStream);
                }

                menuItemFromDb.Image = @"\images\MenuItemsImages\" + menuItemFromDb.Id + "_" + menuItemFromDb.Name + extension;
            }
            //no image? keep the old one...            
           
            //saving!!!
            _unitOfWork.Save();

            return RedirectToAction("MenuItem");
            //return Content("is it working?..");
            //return Content(model_details);
        }

        [HttpGet]
        public IActionResult MenuItemDelete(int? id)
        {
            if(id == null)
            {
                return Json(new { success = false, message = "MenuItem Not Found"});
            }

            MenuItem menuItem = _unitOfWork.MenuItem.Get(x => x.Id == id);
            if (menuItem == null)
            {
                return Json(new { succuess = false, message = "MenuItem not Found" });
            }
            try
            {
                _unitOfWork.MenuItem.Delete(menuItem);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return Json(new {success = false, message = "An error occured while deleting!!!"});
            }

            return Json(new { success = true, message = "Item deleted successfully" });
            
        }
    }
}
