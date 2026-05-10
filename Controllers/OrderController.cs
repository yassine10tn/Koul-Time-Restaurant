using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Data.Interfaces;
using RestaurantApp.Models;
using RestaurantApp.ViewModels;
using RestaurantApp.Filters;
using System.Text.Json;

namespace RestaurantApp.Controllers
{
    [AuthFilter]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IMenuRepository _menuRepo;
        private const string CartSessionKey = "Cart";

        public OrderController(IOrderRepository orderRepo, IMenuRepository menuRepo)
        {
            _orderRepo = orderRepo;
            _menuRepo = menuRepo;
        }

        private Cart GetCart()
        {
            var json = HttpContext.Session.GetString(CartSessionKey);
            return json == null ? new Cart() : JsonSerializer.Deserialize<Cart>(json)!;
        }

        [HttpPost]
        public IActionResult PlaceOrder(CheckoutViewModel vm)
        {
            // Si Pickup, l'adresse n'est pas requise → on la vide et on ignore sa validation
            if (vm.DeliveryType == DeliveryType.Pickup)
            {
                vm.CustomerAddress = "Pickup en restaurant";
                ModelState.Remove("CustomerAddress");
            }

            if (!ModelState.IsValid)
            {
                vm.Cart = GetCart();
                return View("~/Views/Cart/Checkout.cshtml", vm);
            }

            var cart = GetCart();
            if (!cart.Items.Any())
                return RedirectToAction("Index", "Cart");

            var order = new Order
            {
                CustomerName = vm.CustomerName,
                CustomerPhone = vm.CustomerPhone,
                CustomerEmail = vm.CustomerEmail,
                CustomerAddress = vm.CustomerAddress,
                DeliveryType = vm.DeliveryType,
                Notes = vm.Notes,
                Total = cart.Total,
                Items = cart.Items.Select(i => new OrderItem
                {
                    MenuItemId = i.MenuItemId,
                    Name = i.Name,
                    Price = i.Price,
                    Quantity = i.Quantity
                }).ToList()
            };

            var created = _orderRepo.Create(order);
            HttpContext.Session.Remove(CartSessionKey);

            return RedirectToAction("Track", new { id = created.Id });
        }

        public IActionResult Track(int id)
        {
            var order = _orderRepo.GetById(id);
            if (order == null) return NotFound();
            return View(new OrderTrackingViewModel { Order = order });
        }

        [HttpPost]
        public IActionResult NextStep(int id)
        {
            var order = _orderRepo.GetById(id);
            if (order == null) return NotFound();

            if (order.Status < OrderStatus.Livre)
            {
                _orderRepo.UpdateStatus(id, order.Status + 1);
            }

            return RedirectToAction("Track", new { id });
        }

        public IActionResult History()
        {
            var orders = _orderRepo.GetAll();
            return View(orders);
        }
    }
}