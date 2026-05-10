using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Data.Interfaces;
using RestaurantApp.Models;
using RestaurantApp.ViewModels;
using RestaurantApp.Filters;
using System.Text.Json;

namespace RestaurantApp.Controllers
{
    [AuthFilter]
    public class CartController : Controller
    {
        private readonly IMenuRepository _menuRepo;
        private const string CartSessionKey = "Cart";

        public CartController(IMenuRepository menuRepo)
        {
            _menuRepo = menuRepo;
        }

        private Cart GetCart()
        {
            var json = HttpContext.Session.GetString(CartSessionKey);
            return json == null ? new Cart() : JsonSerializer.Deserialize<Cart>(json)!;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
        }

        public IActionResult Index()
        {
            return View(GetCart());
        }

        [HttpPost]
        public IActionResult Add(int menuItemId, int quantity = 1)
        {
            var menuItem = _menuRepo.GetById(menuItemId);
            if (menuItem == null) return NotFound();

            var cart = GetCart();
            cart.AddItem(menuItem, quantity);
            SaveCart(cart);

            TempData["Success"] = $"{menuItem.Name} ajouté au panier !";
            return RedirectToAction("Index", "Menu");
        }

        [HttpPost]
        public IActionResult Remove(int menuItemId)
        {
            var cart = GetCart();
            cart.RemoveItem(menuItemId);
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int menuItemId, int quantity)
        {
            var cart = GetCart();
            cart.UpdateQuantity(menuItemId, quantity);
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Clear()
        {
            HttpContext.Session.Remove(CartSessionKey);
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cart = GetCart();
            if (!cart.Items.Any())
                return RedirectToAction("Index");

            return View(new CheckoutViewModel { Cart = cart });
        }
    }
}