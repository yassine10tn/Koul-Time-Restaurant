using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Data.Interfaces;
using RestaurantApp.Models;
using RestaurantApp.ViewModels;
using RestaurantApp.Filters;

namespace RestaurantApp.Controllers
{
    [AuthFilter]
    public class AdminController : Controller
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IMenuRepository _menuRepo;
        private readonly IUserRepository _userRepo;

        public AdminController(IOrderRepository orderRepo, IMenuRepository menuRepo, IUserRepository userRepo)
        {
            _orderRepo = orderRepo;
            _menuRepo = menuRepo;
            _userRepo = userRepo;
        }

        private bool IsAdmin() => HttpContext.Session.GetString("UserRole") == "Admin";

        // ─── Dashboard ───────────────────────────────────────────────
        public IActionResult Dashboard()
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Menu");

            var orders = _orderRepo.GetAll().ToList();
            var vm = new AdminDashboardViewModel
            {
                Orders = orders,
                TotalOrders = orders.Count,
                OrdersEnCours = orders.Count(o => o.Status != OrderStatus.Livre
                                               && o.Status != OrderStatus.Annule),
                OrdersLivres = orders.Count(o => o.Status == OrderStatus.Livre),
                TotalRevenue = orders.Where(o => o.Status != OrderStatus.Annule)
                                      .Sum(o => o.Total)
            };
            return View(vm);
        }

        // ─── Orders ──────────────────────────────────────────────────
        public IActionResult Orders()
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Menu");
            return View(_orderRepo.GetAll());
        }

        [HttpPost]
        public IActionResult UpdateStatus(int id, OrderStatus status)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Menu");
            _orderRepo.UpdateStatus(id, status);
            TempData["Success"] = "Statut mis à jour !";
            return RedirectToAction("Orders");
        }

        // ─── Menu ────────────────────────────────────────────────────
        public IActionResult Menu()
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Menu");
            var vm = new AdminMenuViewModel
            {
                Items = _menuRepo.GetAll(),
                Categories = _menuRepo.GetCategories()
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddMenuItem(MenuItem item)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Menu");
            if (ModelState.IsValid)
            {
                _menuRepo.Add(item);
                TempData["Success"] = "Plat ajouté avec succès !";
            }
            return RedirectToAction("Menu");
        }

        [HttpPost]
        public IActionResult DeleteMenuItem(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Menu");
            _menuRepo.Delete(id);
            TempData["Success"] = "Plat supprimé.";
            return RedirectToAction("Menu");
        }

        // ─── Users ───────────────────────────────────────────────────
        public IActionResult Users()
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Menu");
            return View(_userRepo.GetAll());
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Menu");
            if (!string.IsNullOrWhiteSpace(user.Email) && !string.IsNullOrWhiteSpace(user.Password))
            {
                var newUser = new User
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    Password = user.Password,
                    Role = user.Role
                };
                _userRepo.Register(newUser);
                TempData["Success"] = "Utilisateur ajouté !";
            }
            return RedirectToAction("Users");
        }

        [HttpPost]
        public IActionResult EditUser(User user)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Menu");
            _userRepo.Update(user);
            TempData["Success"] = "Utilisateur modifié !";
            return RedirectToAction("Users");
        }

        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Menu");
            var currentUserId = HttpContext.Session.GetInt32("UserId");
            if (currentUserId == id)
            {
                TempData["Error"] = "Vous ne pouvez pas supprimer votre propre compte !";
                return RedirectToAction("Users");
            }
            _userRepo.Delete(id);
            TempData["Success"] = "Utilisateur supprimé.";
            return RedirectToAction("Users");
        }

        [HttpPost]
        public IActionResult ChangeRole(int id, UserRole role)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Menu");
            var currentUserId = HttpContext.Session.GetInt32("UserId");
            if (currentUserId == id)
            {
                TempData["Error"] = "Vous ne pouvez pas modifier votre propre rôle !";
                return RedirectToAction("Users");
            }
            var user = _userRepo.GetById(id);
            if (user != null)
            {
                user.Role = role;
                _userRepo.Update(user);
                TempData["Success"] = "Rôle modifié !";
            }
            return RedirectToAction("Users");
        }
    }
}