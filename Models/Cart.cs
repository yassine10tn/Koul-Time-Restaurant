using System.Text.Json.Serialization;

namespace RestaurantApp.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new();

        [JsonIgnore]
        public decimal Total => Items.Sum(i => i.Total);

        [JsonIgnore]
        public int ItemCount => Items.Sum(i => i.Quantity);

        public void AddItem(MenuItem menuItem, int quantity = 1)
        {
            var existing = Items.FirstOrDefault(i => i.MenuItemId == menuItem.Id);
            if (existing != null)
                existing.Quantity += quantity;
            else
                Items.Add(new CartItem
                {
                    MenuItemId = menuItem.Id,
                    Name = menuItem.Name,
                    Price = menuItem.Price,
                    Quantity = quantity
                });
        }

        public void RemoveItem(int menuItemId)
        {
            var item = Items.FirstOrDefault(i => i.MenuItemId == menuItemId);
            if (item != null) Items.Remove(item);
        }

        public void UpdateQuantity(int menuItemId, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.MenuItemId == menuItemId);
            if (item != null)
            {
                if (quantity <= 0) Items.Remove(item);
                else item.Quantity = quantity;
            }
        }

        public void Clear() => Items.Clear();
    }
}