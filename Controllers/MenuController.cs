using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Data.Interfaces;
using RestaurantApp.ViewModels;
using RestaurantApp.Filters;

namespace RestaurantApp.Controllers
{
    [AuthFilter]
    public class MenuController : Controller
    {
        private readonly IMenuRepository _menuRepo;

        public MenuController(IMenuRepository menuRepo)
        {
            _menuRepo = menuRepo;
        }

        public IActionResult Index(int? categoryId, string? search)
        {
            var items = categoryId.HasValue
                ? _menuRepo.GetByCategory(categoryId.Value)
                : _menuRepo.GetAll();

            if (!string.IsNullOrEmpty(search))
                items = items.Where(i =>
                    i.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    i.Description.Contains(search, StringComparison.OrdinalIgnoreCase));

            var vm = new MenuViewModel
            {
                Items = items,
                Categories = _menuRepo.GetCategories(),
                SelectedCategoryId = categoryId,
                SearchQuery = search
            };

            return View(vm);
        }

        public IActionResult Detail(int id)
        {
            var item = _menuRepo.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }
    }
}