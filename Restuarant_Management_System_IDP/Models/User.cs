using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restuarant_Management_System_IDP.Models
{
    public class User
    {
        [Key,MaxLength(15)]
        public string CustomerId { get; set; }

        [Required(ErrorMessage = "Please enter your Name")]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email Address is Required")]
        [MinLength(5,ErrorMessage = "Email length must be greater than 5")]
        [MaxLength(30)]
        [EmailAddress(ErrorMessage = "Invalid Error Address")]
        public string Email { get; set; }

        [Required]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"[7-9]{1}[0-9]{9}",ErrorMessage = "Invalid mobile no")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string Contact {  get; set; }

        //[DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string? Contact2 { get; set; }

        public ICollection<Addresstb> Addresses { get; set; } //navigational property
    }
}
