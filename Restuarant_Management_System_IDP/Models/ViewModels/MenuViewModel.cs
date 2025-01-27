using Microsoft.AspNetCore.Mvc.Rendering;

namespace Restuarant_Management_System_IDP.Models.ViewModels
{
    public class MenuViewModel
    {
        public List<Category> CategoryList { get; set; }
        public Dictionary<int, List<SelectListItem>> SubCategoryDict { get; set; }
        public List<SelectListItem> categorySelectList { get; set; }

        //public 
    }
}
