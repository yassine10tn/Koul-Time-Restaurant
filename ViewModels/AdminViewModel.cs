using RestaurantApp.Models;

namespace RestaurantApp.ViewModels
{
    public class AdminDashboardViewModel
    {
        public IEnumerable<Order> Orders { get; set; } = new List<Order>();
        public int TotalOrders { get; set; }
        public int OrdersEnCours { get; set; }
        public int OrdersLivres { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class AdminMenuViewModel
    {
        public IEnumerable<MenuItem> Items { get; set; } = new List<MenuItem>();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public MenuItem NewItem { get; set; } = new();
    }
}