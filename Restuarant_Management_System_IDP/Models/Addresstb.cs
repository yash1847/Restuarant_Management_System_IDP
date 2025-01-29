using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restuarant_Management_System_IDP.Models
{
    public class Addresstb
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Addresss is required")]
        [MaxLength(50)]
        [MinLength(10, ErrorMessage = "Address must be greater than 10 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City name is required")]
        [MaxLength(15)]
        public string City { get; set; }

        [Required(ErrorMessage = "Pincode is required")]
        [MaxLength(6,ErrorMessage = "Invalid PinCode")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Pincode must be exactly 6 digits.")]
        public string Pincode { get; set; }

        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; } //navigation property
    }
}
