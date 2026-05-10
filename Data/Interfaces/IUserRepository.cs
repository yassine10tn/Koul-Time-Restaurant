using RestaurantApp.Models;

namespace RestaurantApp.Data.Interfaces
{
    public interface IUserRepository
    {
        User? GetByEmail(string email);
        User? Authenticate(string email, string password);
        void Register(User user);
        IEnumerable<User> GetAll();
        User? GetById(int id);
        void Update(User user);
        void Delete(int id);
    }
}