using System.ComponentModel.DataAnnotations;

namespace Restuarant_Management_System_IDP.Models
{
    public class RegistrationViewModel
    {
        public Addresstb Addresstb { get; set; }
        public Login Login { get; set; }
        public User User { get; set; }
    }
}
