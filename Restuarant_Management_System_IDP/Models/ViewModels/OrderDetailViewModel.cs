namespace Restuarant_Management_System_IDP.Models.ViewModels
{
    public class OrderDetailViewModel
    {
        public OrderHeader OrderHeader { get; set; }
        public List<OrderDetail> orderDetails { get; set; }
    }
}
