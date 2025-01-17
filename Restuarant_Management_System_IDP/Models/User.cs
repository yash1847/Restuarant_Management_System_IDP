using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restuarant_Management_System_IDP.Models
{
    public class User
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(20,MinimumLength = 5,ErrorMessage = "Username must be betweeen 5 to 15 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Fullname is required")]
        [MaxLength(25)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email id is required")]
        [MaxLength(30, ErrorMessage = "Email address can't be more than 30 characters")]
        [EmailAddress(ErrorMessage = "Invalid Error Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact no is required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"[7-9]{1}[0-9]{9}",ErrorMessage = "Invalid mobile no")]
        public string Contact {  get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Password must be greater than 5 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }


        public ICollection<Addresstb> Addresses { get; set; } //navigational property
    }
}
