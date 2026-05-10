using RestaurantApp.Models;

namespace RestaurantApp.ViewModels
{
    public class MenuViewModel
    {
        public IEnumerable<MenuItem> Items { get; set; } = new List<MenuItem>();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public int? SelectedCategoryId { get; set; }
        public string? SearchQuery { get; set; }
    }
}