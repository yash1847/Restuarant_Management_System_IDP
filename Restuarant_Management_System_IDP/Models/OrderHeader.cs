using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restuarant_Management_System_IDP.Models
{
    public class OrderHeader //contains info about particular order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "Order Total")]
        public double OrderTotal { get; set; }

        [Required]
        [Display(Name = "Pickup Time")]
        [DataType(DataType.Time)]
        public DateTime PickUpTime { get; set; }

        [Required]
        [Display(Name = "Pickup Date")]
        [DataType(DataType.Date)]
        public DateTime PickUpDate { get; set; }

        public string Status { get; set; }
        public string PaymentStatus { get; set; }

        //public string Comments { get; set; }

        //[Display(Name = "Phone Number")]
        //public string PhoneNumber { get; set; }

        //public string TransactionId { get; set; } //payment

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
