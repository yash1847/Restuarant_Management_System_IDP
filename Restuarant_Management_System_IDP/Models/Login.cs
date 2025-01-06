using System.ComponentModel.DataAnnotations;

namespace Restuarant_Management_System_IDP.Models
{
    public class Login
    {
        [Key,MaxLength(20)]
        public string LoginId { get; set; }
        
        [Required,MaxLength(30),DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required,MaxLength(20),DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role {  get; set; }
    }
}
