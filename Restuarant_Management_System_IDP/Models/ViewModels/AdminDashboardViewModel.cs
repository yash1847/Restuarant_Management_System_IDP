namespace Restuarant_Management_System_IDP.Models.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int OrdersSubmitted { get; set; }
        public int OrdersonDelivery { get; set; }
        public int OrdersCompleted { get; set; }
        public int UsersRegistered { get; set; }
        public int EmployeesRegistered { get; set; }
        public double TotalSales { get; set; }
    }
}
