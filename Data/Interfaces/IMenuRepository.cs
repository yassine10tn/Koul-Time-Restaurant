using RestaurantApp.Models;

namespace RestaurantApp.Data.Interfaces
{
    public interface IMenuRepository
    {
        IEnumerable<MenuItem> GetAll();
        IEnumerable<MenuItem> GetByCategory(int categoryId);
        MenuItem? GetById(int id);
        IEnumerable<Category> GetCategories();
        void Add(MenuItem item);
        void Update(MenuItem item);
        void Delete(int id);
    }
}