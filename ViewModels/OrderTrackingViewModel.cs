using RestaurantApp.Models;

namespace RestaurantApp.ViewModels
{
    public class OrderTrackingViewModel
    {
        public Order Order { get; set; } = new();

        public List<OrderStep> Steps { get; set; } = new()
        {
            new OrderStep { Status = OrderStatus.EnCours,       Label = "Commande reçue",   Icon = "✅" },
            new OrderStep { Status = OrderStatus.EnPreparation, Label = "En préparation",   Icon = "👨‍🍳" },
            new OrderStep { Status = OrderStatus.EnLivraison,   Label = "En livraison",     Icon = "🚴" },
            new OrderStep { Status = OrderStatus.Livre,         Label = "Livré",            Icon = "🎉" },
        };

        public bool IsStepDone(OrderStatus step)
            => Order.Status >= step;
    }

    public class OrderStep
    {
        public OrderStatus Status { get; set; }
        public string Label { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }
}