using Microsoft.AspNetCore.Mvc.Rendering;

namespace Restuarant_Management_System_IDP.Models.ViewModels
{
    public class MenuItemViewModel
    {
        public MenuItem MenuItem { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
        public String StatusMessage { get; set; }
    }
}
