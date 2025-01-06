using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restuarant_Management_System_IDP.Models
{
    public enum AddressTypes
    {
        Personal,
        Work
    }
    public class Addresstb
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }

        [ForeignKey("User")]
        public int CustomerId { get; set; }

        [Required]
        public  AddressTypes AddressType { get; set; }

        [Required(ErrorMessage = "Please enter you addresss")]
        [MaxLength(50)]
        [MinLength(10, ErrorMessage = "Address must be greater than 10 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter you city name")]
        [MaxLength(15)]
        public string City { get; set; }

        [Required(ErrorMessage = "Enter your pincode")]
        [MaxLength(6,ErrorMessage = "Invalid PinCode")]
        public string Pincode { get; set; } 

        public User User { get; set; } //navigation property
    }
}
