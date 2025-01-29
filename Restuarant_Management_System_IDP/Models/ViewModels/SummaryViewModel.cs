using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Restuarant_Management_System_IDP.Models.ViewModels
{
    public class SummaryViewModel
    {

        public OrderHeader OrderHeader { get; set; }
        public Addresstb Address { get; set; }

        [ValidateNever]
        public List<ShoppingCart> cartList { get; set; }
    }
}
