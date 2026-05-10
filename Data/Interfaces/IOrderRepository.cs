using RestaurantApp.Models;

namespace RestaurantApp.Data.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order? GetById(int id);
        Order Create(Order order);
        void UpdateStatus(int id, OrderStatus status);
    }
}