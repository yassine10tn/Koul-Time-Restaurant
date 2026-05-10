using RestaurantApp.Data.Interfaces;
using RestaurantApp.Models;

namespace RestaurantApp.Data.Mock
{
    public class MockUserRepository : IUserRepository
    {
        private static List<User> _users = new()
        {
            new User { Id=1, FullName="Admin",        Email="admin@restaurant.com", Password="admin123",  Role=UserRole.Admin  },
            new User { Id=2, FullName="Jean Dupont",  Email="client@test.com",      Password="client123", Role=UserRole.Client },
            new User { Id=3, FullName="Yassine Amri", Email="yassine@test.com",     Password="pass123",   Role=UserRole.Client },
            new User { Id=4, FullName="Adem Hamroun", Email="adem@test.com",        Password="pass123",   Role=UserRole.Admin  },
            new User { Id=5, FullName="Sarra Ben Ali",Email="sarra@test.com",       Password="pass123",   Role=UserRole.Client },
        };

        public User? GetByEmail(string email) =>
            _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        public User? Authenticate(string email, string password) =>
            _users.FirstOrDefault(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);

        public User? GetById(int id) =>
            _users.FirstOrDefault(u => u.Id == id);

        public void Register(User user)
        {
            user.Id = _users.Max(u => u.Id) + 1;
            _users.Add(user);
        }

        public void Update(User user)
        {
            var existing = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existing != null)
            {
                existing.FullName = user.FullName;
                existing.Email = user.Email;
                existing.Role = user.Role;
                if (!string.IsNullOrWhiteSpace(user.Password))
                    existing.Password = user.Password;
            }
        }

        public void Delete(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null) _users.Remove(user);
        }

        public IEnumerable<User> GetAll() => _users;
    }
}