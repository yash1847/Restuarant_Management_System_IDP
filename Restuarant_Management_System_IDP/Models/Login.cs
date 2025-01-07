using System.ComponentModel.DataAnnotations;

namespace Restuarant_Management_System_IDP.Models
{
    public enum Roles
    {
        Customer,
        Admin
    }
    public class Login
    {
        [Key]
        [MaxLength(15,ErrorMessage = "UserId must be less than 15 characters")]
        public string LoginId { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [MinLength(5, ErrorMessage = "Password must be greater than 5 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public Roles Role {  get; set; }
    }
}
