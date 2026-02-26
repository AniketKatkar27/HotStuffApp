namespace HotStuffApp.Models.ViewModels
{
    public class AdminDashboardVM
    {
        public int TotalOrders { get; set; }
        public int TotalProducts {  get; set; }
        public int TotalCategories { get; set; }
        public int TotalCustomers { get; set; }
        public decimal TotalRevenue { get; set; }

        public List<Order>? RecentOrders { get; set; }
    }
}
