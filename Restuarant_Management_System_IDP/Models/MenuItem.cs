using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restuarant_Management_System_IDP.Models
{
    public class MenuItem
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        [Required]
        [Display(Name = "Category")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "SubCategory")]
        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; } 

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = " Price should be greater than ${1}")]
        public double Price { get; set; }

        public virtual Category Category { get; set; } //navigational property
        public virtual SubCategory SubCategory { get; set; } //navigational property
        //public virtual ShoppingCart ShoppingCart { get; set; } //nav

    }
}
