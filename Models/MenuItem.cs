namespace RestaurantApp.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; } = "/images/default-food.jpg";
        public bool IsAvailable { get; set; } = true;
    }
}