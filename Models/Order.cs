namespace RestaurantApp.Models
{
    public enum OrderStatus
    {
        EnCours = 0,
        EnPreparation = 1,
        EnLivraison = 2,
        Livre = 3,
        Annule = 4
    }

    public enum DeliveryType
    {
        Livraison = 0,
        Pickup = 1
    }

    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> Items { get; set; } = new();
        public decimal Total { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.EnCours;
        public DeliveryType DeliveryType { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerAddress { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string StatusLabel => Status switch
        {
            OrderStatus.EnCours => "Commande reçue",
            OrderStatus.EnPreparation => "En préparation",
            OrderStatus.EnLivraison => "En livraison",
            OrderStatus.Livre => "Livré",
            OrderStatus.Annule => "Annulé",
            _ => "Inconnu"
        };

        public string StatusColor => Status switch
        {
            OrderStatus.EnCours => "warning",
            OrderStatus.EnPreparation => "info",
            OrderStatus.EnLivraison => "primary",
            OrderStatus.Livre => "success",
            OrderStatus.Annule => "danger",
            _ => "secondary"
        };
    }
}