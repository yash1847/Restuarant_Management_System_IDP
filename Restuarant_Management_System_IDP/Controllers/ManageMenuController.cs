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
        public ManageMenuController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            Category? category = _unitOfWork.Category.Search(id);
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
                return View("Category/Edit");
            }
            //check if category name already exists
            _unitOfWork.Category.Update(category);
            _unitOfWork.Save();
            return RedirectToAction("Category");
        }

        [HttpPost]
        public IActionResult CategoryDelete(int? id)
        {
            Category? category = _unitOfWork.Category.Search(id);
            if(category == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }
            if (category.MenuItems.Any())
            {
                return Json(new { success = false, message = "Category contains menu items!" });
            }
            _unitOfWork.Category.Delete(category);
            _unitOfWork.Save();
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
            string buttonPressed = (string)frm["button"];

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
            List<Category> categories = _unitOfWork.Category.GetAll();
            SubCategoryAndCategoryViewModel model = new SubCategoryAndCategoryViewModel()
            {
                CategoryList = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name}),
                SubCategory = new SubCategory(),
            };
            return View("SubCategory/Create",model);
        }
        
        [HttpPost]
        public IActionResult SubCategoryCreate(SubCategoryAndCategoryViewModel model)
        {
            //ModelState.Remove("StatusMessage");
            //ModelState.Remove("SubCategory.Category");
            //ModelState.Remove("CategoryList");
            //if (ModelState.IsValid)
            //{
                bool subcategoryExists = _unitOfWork.SubCategory.GetAll().Where(s => s.CategoryId == model.SubCategory.CategoryId && s.Name == model.SubCategory.Name).Any();
                if (subcategoryExists)
                {
                    model.StatusMessage = "Error: Sub Category with the same already exists under this category, Please use another Name";
                }
                else
                {
                    //model.SubCategory.Category = _unitOfWork.Category.Search(model.SubCategory.CategoryId);
                    _unitOfWork.SubCategory.Add(model.SubCategory);    
                    _unitOfWork.Save();
                    return RedirectToAction("SubCategory");
                }
            //}
            return View("SubCategory/Create", model);

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

            return View("SubCategory/Details",subcategory);
        }
    }
}
