using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restuarant_Management_System_IDP.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Count = 1;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserID { get; set; }

        [Required]
        [ForeignKey("MenuItem")]
        public int MenuItemId { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Please enter a value greater than or equal to {1}")]
        public int Count { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual MenuItem MenuItem { get; set; }

    }
}
