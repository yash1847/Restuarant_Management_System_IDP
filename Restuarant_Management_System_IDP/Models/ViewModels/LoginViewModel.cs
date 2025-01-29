using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using System.ComponentModel.DataAnnotations;

namespace Restuarant_Management_System_IDP.Models.ViewModels
{
    [Keyless]
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [ValidateNever]
        public String StatusMessage { get; set; }

    }
}
