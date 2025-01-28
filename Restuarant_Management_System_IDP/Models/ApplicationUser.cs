using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restuarant_Management_System_IDP.Models
{
    public class ApplicationUser:IdentityUser
    {
        //public ApplicationUser()
        //{
        //    Addresstbs = new List<Addresstb>();
        //    OrderHeaders = new List<OrderHeader>();
        //    ShoppingCarts = new List<ShoppingCart>();
        //}

        [Required]
        public string FullName { get; set; }


        [NotMapped]
        public string Role { get; set; }

        public virtual  ICollection<Addresstb> Addresstbs { get; set; }
        //public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }
    }
}
