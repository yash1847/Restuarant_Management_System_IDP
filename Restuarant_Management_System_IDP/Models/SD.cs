namespace Restuarant_Management_System_IDP.Models
{
    public static class SD
    {
        //Roles
        public const string Admin = "Admin";
        public const string Kitchen = "KitchenUser";
        public const string Delivery = "Delivery";
        public const string Customer = "Customer";

        //default image
        public const string DefaultFoodImage = "defaultFoodImage.png";

        // Order Status
        public const string StatusSubmitted = "Submitted";
        public const string StatusInProcess = "Being Prepared";
        public const string StatusReady = "On Delivery";
        public const string StatusCompleted = "Delivered";
        //public const string StatusCancelled = "Cancelled";

        // Payment Status
        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusRejected = "Rejected";

    }
}
