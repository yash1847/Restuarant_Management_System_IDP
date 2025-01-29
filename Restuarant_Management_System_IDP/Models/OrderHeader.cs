using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        public string PickupName { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "Order Total")]
        public double OrderTotal { get; set; }

        [Display(Name = "Pickup Time")]
        [DataType(DataType.Time)]
        public DateTime DeliveryTime { get; set; }

        public string Status { get; set; }
        public string PaymentStatus { get; set; }

        //can get from addresstb but if the user changes address in future, it will not affect the order history

        [Required]
        public string DeliverAddress { get; set; }

        [Required]
        public string DeliverCity { get; set; }

        [Required]
        public string DeliverPinCode { get; set; }

        //[ValidateNever]
        //[Required]
        //[ForeignKey("Addresstb")]
        //public int DeliveryAddressId { get; set; }

        //public string TransactionId { get; set; } //payment

        [ValidateNever]
        public virtual ApplicationUser ApplicationUser { get; set; }

        //[ValidateNever]
        //public virtual Addresstb Addresstb { get; set; }
    }
}
