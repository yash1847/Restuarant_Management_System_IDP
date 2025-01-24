using Microsoft.AspNetCore.Mvc.Rendering;
using Restuarant_Management_System_IDP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restuarant_Management_System_IDP.Models.ViewModels
{
    public class SubCategoryAndCategoryViewModel
    {


        public IEnumerable<SelectListItem> CategoryList { get; set; }
        //public SelectList CategoryList { get; set; }
        public SubCategory SubCategory { get; set; }
        //public List<string> SubCategoryList { get; set; }
        public string StatusMessage { get; set; }
    }
}
