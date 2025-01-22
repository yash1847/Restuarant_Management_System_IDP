using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restuarant_Management_System_IDP.Models
{
    public class SubCategory
    {
        public SubCategory()
        {
            MenuItems = new List<MenuItem>();
        }
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "SubCategory Name")]
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Category")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}
