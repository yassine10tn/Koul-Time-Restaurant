using RestaurantApp.Data.Interfaces;
using RestaurantApp.Models;

namespace RestaurantApp.Data.Mock
{
    public class MockMenuRepository : IMenuRepository
    {
        private static List<Category> _categories = new()
        {
            new Category { Id = 1, Name = "Entrées", Icon = "🥗" },
            new Category { Id = 2, Name = "Plats principaux", Icon = "🍖" },
            new Category { Id = 3, Name = "Pizzas", Icon = "🍕" },
            new Category { Id = 4, Name = "Desserts", Icon = "🍰" },
            new Category { Id = 5, Name = "Boissons", Icon = "🥤" }
        };

        private static List<MenuItem> _items = new()
            {
                new MenuItem { Id=1,  Name="Salade César",       Description="Laitue, parmesan, croûtons",              Price=12.500m, CategoryId=1 },
                new MenuItem { Id=2,  Name="Soupe du jour",       Description="Soupe maison du chef",                    Price=8.000m,  CategoryId=1 },
                new MenuItem { Id=3,  Name="Entrecôte grillée",   Description="300g, frites maison",                     Price=38.000m, CategoryId=2 },
                new MenuItem { Id=4,  Name="Poulet rôti",         Description="Demi poulet, légumes",                    Price=24.500m, CategoryId=2 },
                new MenuItem { Id=5,  Name="Pizza Margherita",    Description="Tomate, mozzarella, basilic",             Price=19.000m, CategoryId=3 },
                new MenuItem { Id=6,  Name="Pizza 4 fromages",    Description="Mozzarella, gorgonzola, chèvre, emmental",Price=22.500m, CategoryId=3 },
                new MenuItem { Id=7,  Name="Tiramisu",            Description="Recette traditionnelle italienne",         Price=10.000m, CategoryId=4 },
                new MenuItem { Id=8,  Name="Fondant chocolat",    Description="Cœur coulant, glace vanille",             Price=11.500m, CategoryId=4 },
                new MenuItem { Id=9,  Name="Eau minérale",        Description="50cl",                                    Price=2.500m,  CategoryId=5 },
                new MenuItem { Id=10, Name="Jus d'orange",        Description="Pressé maison",                           Price=6.000m,  CategoryId=5 },
            };

        public IEnumerable<MenuItem> GetAll() => _items.Where(i => i.IsAvailable);
        public IEnumerable<MenuItem> GetByCategory(int categoryId) => _items.Where(i => i.CategoryId == categoryId && i.IsAvailable);
        public MenuItem? GetById(int id) => _items.FirstOrDefault(i => i.Id == id);
        public IEnumerable<Category> GetCategories() => _categories;

        public void Add(MenuItem item)
        {
            item.Id = _items.Max(i => i.Id) + 1;
            _items.Add(item);
        }

        public void Update(MenuItem item)
        {
            var existing = _items.FirstOrDefault(i => i.Id == item.Id);
            if (existing != null)
            {
                existing.Name = item.Name;
                existing.Description = item.Description;
                existing.Price = item.Price;
                existing.CategoryId = item.CategoryId;
                existing.IsAvailable = item.IsAvailable;
            }
        }

        public void Delete(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            if (item != null) item.IsAvailable = false;
        }
    }
}