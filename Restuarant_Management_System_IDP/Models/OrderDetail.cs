using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restuarant_Management_System_IDP.Models
{
    public class OrderDetail //contains info about order details
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("OrderHeader")]
        public int OrderId { get; set; }

        [Required]
        [ForeignKey("MenuItem")]
        public int MenuItemId { get; set; }

        [Required]
        public int Count { get; set; } //unique menu items

        //can get from menuItem but if the admin changes the price or name in future, it will not affect the order history

        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        public virtual OrderHeader OrderHeader { get; set; } //navigation property
        public virtual MenuItem MenuItem { get; set; } //navigation property

    }
}
