using Restuarant_Management_System_IDP.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Restuarant_Management_System_IDP.Models
{
    public class Category
    {
        public Category()
        {
            MenuItems = new List<MenuItem>();
            SubCategories = new List<SubCategory>();
        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "Category Name")]
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<MenuItem> MenuItems { get; set; } //nav

        [JsonIgnore]
        public virtual ICollection<SubCategory> SubCategories { get; set; } //nav
    }
}
