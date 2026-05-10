using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Data.Interfaces;
using RestaurantApp.Models;
using RestaurantApp.ViewModels;

namespace RestaurantApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepo;
        private const string SessionUserKey = "UserId";
        private const string SessionRoleKey = "UserRole";
        private const string SessionNameKey = "UserName";

        public AuthController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32(SessionUserKey) != null)
                return RedirectToAction("Index", "Menu");

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = _userRepo.Authenticate(vm.Email, vm.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Email ou mot de passe incorrect");
                return View(vm);
            }

            HttpContext.Session.SetInt32(SessionUserKey, user.Id);
            HttpContext.Session.SetString(SessionRoleKey, user.Role.ToString());
            HttpContext.Session.SetString(SessionNameKey, user.FullName);

            TempData["Success"] = $"Bienvenue {user.FullName} !";

            return user.Role == UserRole.Admin
                ? RedirectToAction("Dashboard", "Admin")
                : RedirectToAction("Index", "Menu");
        }

        public IActionResult Register()
        {
            if (HttpContext.Session.GetInt32(SessionUserKey) != null)
                return RedirectToAction("Index", "Menu");

            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            if (_userRepo.GetByEmail(vm.Email) != null)
            {
                ModelState.AddModelError("Email", "Cet email est déjà utilisé");
                return View(vm);
            }

            _userRepo.Register(new User
            {
                FullName = vm.FullName,
                Email = vm.Email,
                Password = vm.Password
            });

            TempData["Success"] = "Compte créé ! Connectez-vous.";
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Success"] = "Déconnexion réussie.";
            return RedirectToAction("Login");
        }
    }
}