using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Restuarant_Management_System_IDP.Models.ViewModels
{
    [Keyless]
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Enter your username")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Username must be betweeen 5 to 15 characters")]
        
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter your name")]
        [MaxLength(25)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Enter your email id")]
        [MaxLength(30, ErrorMessage = "Email address can't be more than 30 characters")]
        [EmailAddress(ErrorMessage = "Invalid Error Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your contact no")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"[7-9]{1}[0-9]{9}", ErrorMessage = "Invalid mobile no")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [ValidateNever]
        public string Role { get; set; } = "Customer";
    }
}
