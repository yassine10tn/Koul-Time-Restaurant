using RestaurantApp.Data.Interfaces;
using RestaurantApp.Models;

namespace RestaurantApp.Data.Mock
{
    public class MockOrderRepository : IOrderRepository
    {
        private static List<Order> _orders = new();
        private static int _nextId = 1;

        public IEnumerable<Order> GetAll() => _orders.OrderByDescending(o => o.CreatedAt);
        public Order? GetById(int id) => _orders.FirstOrDefault(o => o.Id == id);

        public Order Create(Order order)
        {
            order.Id = _nextId++;
            _orders.Add(order);
            return order;
        }

        public void UpdateStatus(int id, OrderStatus status)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order != null) order.Status = status;
        }
    }
}